using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sorling.SqlConnAuthWeb.authentication;

namespace Sorling.sqlexecDevWeb.pages.sqlsrv;

public class IndexModel : PageModel
{
   public IEnumerable<SqlConnectionHelper.ListDBCmd.ListDBRes>? DBs { get; private set; }

   public async Task OnGetAsync([FromServices] ISqlConnAuthenticationService sqlAuth) => DBs = await sqlAuth.GetDBsAsync();

}
