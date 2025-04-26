using Sorling.SqlConnAuthWeb.authentication;
using Sorling.SqlExec.runner;

namespace Sorling.sqlexecDevWeb.extensions;

public static class SQLConnAuthenticationServiceExtensions
{
   public static SqlExecRunner SqlExecRunner(this ISqlAuthService auth, string? database = null)
      => new(auth.GetConnectionString(database)
         ?? throw new ApplicationException("Failed to obtain SQL server connection string"));
}
