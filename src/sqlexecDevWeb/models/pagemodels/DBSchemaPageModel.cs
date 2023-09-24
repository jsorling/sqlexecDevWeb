using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sorling.SqlConnAuthWeb.authentication;
using Sorling.SqlExec.runner;
using Sorling.SqlExecMeta;

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

   public async override Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next) {
      try {
         DBSchemas = await SqlMetadataProvider.GetSqlSchemasAsync();
      }
      catch (Exception ex) {
         SchemaError = ex;
      }

      if (SchemaError is null && DBSchemas is not null && !string.IsNullOrEmpty(DBSchema) && !DBSchemas.Any(s => s == DBSchema))
         context.Result = NotFound();
      else
         await base.OnPageHandlerExecutionAsync(context, next);
   }
}
