using Sorling.SqlExec.runner;
using Sorling.sqlexecCodeGen.project.cmds;

namespace Sorling.sqlexecCodeGen.project;
public class SqlExecProject(ISqlExecRunner sqlExecRunner)
{
   private readonly ISqlExecRunner _sqlExecRunner = sqlExecRunner;

   public async Task<bool> ProjectStoreIsInstalledAsync()
      => await _sqlExecRunner.ExecuteScalarAsync<bool, ProjectStoreIsInstalledCmd>(new());
}
