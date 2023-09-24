using Sorling.SqlConnAuthWeb.authentication;
using Sorling.SqlExec.runner;

namespace Sorling.sqlexecDevWeb.extensions;

public static class SQLConnAuthenticationServiceExtensions
{
   public static SqlExecRunner SqlExecRunner(this ISqlConnAuthenticationService auth, string? database = null)
      => new(auth.SqlConnectionString(database));
}
