using Microsoft.AspNetCore.Mvc;
using Sorling.SqlConnAuthWeb.authentication;
using Sorling.sqlexecDevWeb.models.pagemodels;
using Sorling.SqlExecMeta.security;

namespace Sorling.sqlexecDevWeb.pages.sqlsrv;

public class PrincipalsModel : DBSchemaPageModel
{
   public PrincipalsModel(ISqlConnAuthenticationService sqlAuth) : base(sqlAuth) {
   }

   public IEnumerable<SqlPrincipalListItem>? Principals { get; private set; }

   public async Task<IActionResult> OnGetAsync() {
      Principals = await SqlMetadataProvider.GetSqlPrincipalsAsync(null);
      return Page();
   }
}
