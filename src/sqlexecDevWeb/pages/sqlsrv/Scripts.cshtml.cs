using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sorling.SqlConnAuthWeb.authentication;
using Sorling.sqlexecDevWeb.models.inputmodels;
using Sorling.SqlExecMeta.objects;

namespace Sorling.sqlexecDevWeb.pages.sqlsrv;

public class ScriptsModel : PageModel
{
   [BindProperty(Name = "db", SupportsGet = true)]
   public string? DBName { get; set; }

   [BindProperty]
   public ScriptInputModel ScriptInputModel { get; set; } = new();

   public Dictionary<int, IEnumerable<SqlResultSetColumn>>? ResultSets { get; private set; }

   public string? ResultSetSql { get; private set; }

   public void OnGet() => ScriptInputModel = new() { SqlScriptText = "select cast(1 as int) anint;" };

   public async Task<IActionResult> OnPostAsync([FromServices] ISqlAuthService sqlAuth) {
      if (ModelState.IsValid)
      {
         try
         {
            (Dictionary<int, IEnumerable<SqlResultSetColumn>> ResultSets, string QuerySql) rs =
               await new SqlResultSets(sqlAuth.GetConnectionString(DBName)
                  ?? throw new ApplicationException("Failed to obtain SQL server connection string"))
               .GetResultSetsAsync(ScriptInputModel.SqlScriptText ?? "");

            ResultSets = rs.ResultSets;
            ResultSetSql = rs.QuerySql;
         }
         catch (Exception ex)
         {
            ModelState.AddModelError("SqlScriptText", ex.Message);
         }
      }

      return Page();
   }
}
