using Sorling.SqlConnAuthWeb.authentication;
using Sorling.sqlexecDevWeb.models.pagemodels;

namespace Sorling.sqlexecDevWeb.pages.sqlsrv;

public class SchemasModel(ISqlAuthService sqlAuth) : DBSchemaPageModel(sqlAuth)
{
}
