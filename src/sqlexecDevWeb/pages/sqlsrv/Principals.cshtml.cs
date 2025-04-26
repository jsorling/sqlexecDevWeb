using Microsoft.AspNetCore.Mvc;
using Sorling.SqlConnAuthWeb.authentication;
using Sorling.sqlexecDevWeb.models.pagemodels;
using Sorling.SqlExecMeta.security;

namespace Sorling.sqlexecDevWeb.pages.sqlsrv;

public class PrincipalsModel(ISqlAuthService sqlAuth) : DBSchemaPageModel(sqlAuth)
{
   public IEnumerable<SqlPrincipalListItem>? Principals { get; private set; }

   public async Task<IActionResult> OnGetAsync() {
      Principals = await SqlMetadataProvider.GetSqlPrincipalsAsync(null);
      return Page();
   }
}
