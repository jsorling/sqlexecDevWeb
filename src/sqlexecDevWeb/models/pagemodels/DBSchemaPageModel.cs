using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sorling.SqlConnAuthWeb;
using Sorling.SqlConnAuthWeb.authentication;
using Sorling.SqlExec.runner;
using Sorling.sqlexecDevWeb.extensions;
using Sorling.sqlexecDevWeb.extensions.sqlobjects;
using Sorling.SqlExecMeta;
using Sorling.SqlExecMeta.helpers;

namespace Sorling.sqlexecDevWeb.models.pagemodels;

public abstract class DBSchemaPageModel : PageModel
{
   protected readonly ISqlConnAuthenticationService _sqlAuth;

   public DBSchemaPageModel(ISqlConnAuthenticationService sqlAuth)
      => _sqlAuth = sqlAuth;

   protected virtual ISqlMetadataProvider SqlMetadataProvider
      => new SqlMetadataProvider(new SqlExecRunner(_sqlAuth.SqlConnectionString(DBName)));

   //@page "{db}/{schema?}"
   [BindProperty(Name = "db", SupportsGet = true)]
   public string? DBName { get; set; }

   [BindProperty(Name = "schema", SupportsGet = true)]
   public string? DBSchema { get; set; }

   public IEnumerable<string>? DBSchemas { get; private set; }

   public Exception? SchemaError { get; private set; }

   public string? ObjectItem { get; private set; } = null;

   public string? SchemaFolder { get; private set; } = null;

   public SqlGroupFlags? FilterGroup { get; private set; } = null;

   public (string? Schema, string? Name) ObjectItemParts { get; private set; }

   public string PageUrl(string? page, string? schema, string? fqItem, SqlGroupFlags? sqlGroupFlags) {
      RouteValueDictionary routevalues = new() {
         { SqlConnAuthConsts.URLROUTEPARAMSRV, RouteData.Values[SqlConnAuthConsts.URLROUTEPARAMSRV] },
         { SqlConnAuthConsts.URLROUTEPARAMUSR, RouteData.Values[SqlConnAuthConsts.URLROUTEPARAMUSR] },
         {"db", DBName }
      };

      if (schema is null) {
         if (fqItem is not null) {
            routevalues["schema"] = fqItem;
            if (sqlGroupFlags is not null) {
               routevalues["obj"] = ((int)sqlGroupFlags).ToString();
            }
         }
      }
      else {
         routevalues["schema"] = schema;
         if (fqItem is not null) {
            routevalues["obj"] = fqItem;
            if (sqlGroupFlags is not null) {
               routevalues["filter"] = ((int)sqlGroupFlags).ToString();
            }
         }
      }

      string? tor = Url.Page(page ?? RouteData.Values["page"]?.ToString() ?? "index", routevalues);
      return tor is null ? throw new Exception($"Url.Page is null. Route values {string.Join("--", routevalues)}. Page = {page}") : tor;
   }

   public string PageUrl(ISqlItem sqlItem) => PageUrl($"/{_sqlAuth.Options.SqlRootPath}/{sqlItem.GroupFlag.GetPageAction()}"
      , SchemaFolder, sqlItem.FQItemName(), FilterGroup);

   public override void OnPageHandlerSelected(PageHandlerSelectedContext context) {
      string parschema = context.RouteData.Values["schema"]?.ToString() ?? string.Empty;
      string parobj = context.RouteData.Values["obj"]?.ToString() ?? string.Empty;

      if (parschema.Contains('.')) {
         // first (parschema) is object
         ObjectItem = parschema;
         if (int.TryParse(parobj, out int f)) {
            // second (parobj) is filter
            FilterGroup = (SqlGroupFlags)f;
         }
      }
      else if (parschema != string.Empty) {
         // first (parschema) is schema
         SchemaFolder = parschema;
         string parfilter = context.RouteData.Values["filter"]?.ToString() ?? string.Empty;
         if (parobj.Contains('.')) {
            // second (parobject) is object
            ObjectItem = parobj;
            if (int.TryParse(parfilter, out int f)) {
               // third (parfilter) is filter
               FilterGroup = (SqlGroupFlags)f;
            }
         }
         else if (int.TryParse(parobj, out int f)) {
            // second (parobj) is filter
            FilterGroup = (SqlGroupFlags)f;
         }
      }

      ObjectItemParts = SqlIdentifierHelper.GetSchemaNName(ObjectItem);

      base.OnPageHandlerSelected(context);
   }

   public override async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next) {
      try {
         DBSchemas = await SqlMetadataProvider.GetSqlSchemasAsync();
      }
      catch (Exception ex) {
         SchemaError = ex;
      }

      if (SchemaError is null && DBSchemas is not null && !string.IsNullOrEmpty(SchemaFolder) && !DBSchemas.Any(s => s == SchemaFolder))
         context.Result = NotFound();
      else
         await base.OnPageHandlerExecutionAsync(context, next);
   }
}
