using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sorling.SqlConnAuthWeb.authentication;
using Sorling.SqlExec.runner;
using Sorling.sqlexecDevWeb.filters;
using Sorling.SqlExecMeta;

namespace Sorling.sqlexecDevWeb.models.pagemodels;

[SchemaNObjectFilter]
public abstract class DBSchemaPageModel(ISqlConnAuthenticationService sqlAuth) : PageModel
{
   protected readonly ISqlConnAuthenticationService _sqlAuth = sqlAuth;

   protected virtual ISqlMetadataProvider SqlMetadataProvider
      => new SqlMetadataProvider(new SqlExecRunner(_sqlAuth.SqlConnectionString(DBName)));

   //@page "{db}/{schema?}"
   [BindProperty(Name = "db", SupportsGet = true)]
   public string? DBName { get; set; }
   
   public IEnumerable<string>? DBSchemas { get; private set; }

   public Exception? SchemaError { get; private set; }

   public string? ObjectItem => RouteData.Values[RouteDataKeysConsts.REQSQLOBJECTKEY]?.ToString();

   public SqlGroupFlags? PageGroupFlags => (SqlGroupFlags?)RouteData.Values[RouteDataKeysConsts.REQSQLPAGEGROUPKEY];

   public (string schema, string name)? ObejctItemParts {
      get {
         string? fullname = ObjectItem;
         if (string.IsNullOrEmpty(fullname)) {
            return null;
         }

         string[] sa = fullname.Split('.');
         return sa.Length < 2 ? null
            : ((string Schema, string Name))(sa[^2], sa[^1]);
      }
   }

   public string? SchemaFolder => RouteData.Values[RouteDataKeysConsts.REQSQLSCHEMAKEY]?.ToString();

   public SqlGroupFlags? FilterGroup => (SqlGroupFlags?)(RouteData.Values[RouteDataKeysConsts.REQSQLFILTERKEY] ?? null);

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
