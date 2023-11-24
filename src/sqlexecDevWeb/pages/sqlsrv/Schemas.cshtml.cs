using Sorling.SqlConnAuthWeb.authentication;
using Sorling.sqlexecDevWeb.models.pagemodels;

namespace Sorling.sqlexecDevWeb.pages.sqlsrv;

public class SchemasModel(ISqlConnAuthenticationService sqlAuth) : DBSchemaPageModel(sqlAuth)
{
}
