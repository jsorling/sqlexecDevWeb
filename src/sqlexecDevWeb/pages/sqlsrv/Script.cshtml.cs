using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sorling.SqlConnAuthWeb.authentication;
using Sorling.sqlexecDevWeb.models.inputmodels;
using Sorling.SqlExecMeta.objects;

namespace Sorling.sqlexecDevWeb.pages.sqlsrv;

public class ScriptModel : PageModel
{
   [BindProperty(Name = "db", SupportsGet = true)]
   public string? DBName { get; set; }

   [BindProperty]
   public ScriptInputModel? ScriptInputModel { get; set; }

   public Dictionary<int, IEnumerable<SqlResultSetColumn>>? ResultSets { get; private set; }

   public string? ResultSetSql { get; private set; }

   public void OnGet() => ScriptInputModel = new() { SqlScriptText = "select cast(1 as int) anint;" };

   public async Task<IActionResult> OnPostAsync([FromServices] ISqlConnAuthenticationService sqlAuth) {
      if (ModelState.IsValid) {
         try {
            (Dictionary<int, IEnumerable<SqlResultSetColumn>> ResultSets, string QuerySql) rs =
               await new SqlResultSets(sqlAuth.SqlConnectionString(DBName)).GetResultSetsAsync(ScriptInputModel!.SqlScriptText!);

            ResultSets = rs.ResultSets;
            ResultSetSql = rs.QuerySql;
         }
         catch (Exception ex) {
            ModelState.AddModelError("SqlScriptText", ex.Message);
         }
      }

      return Page();
   }
}
