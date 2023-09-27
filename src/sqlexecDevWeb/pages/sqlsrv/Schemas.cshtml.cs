using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sorling.SqlConnAuthWeb.authentication;
using Sorling.sqlexecDevWeb.models.pagemodels;

namespace Sorling.sqlexecDevWeb.pages.sqlsrv;

public class SchemasModel : DBSchemaPageModel
{
   public SchemasModel(ISqlConnAuthenticationService sqlAuth) : base(sqlAuth) {
   }
}
