using Sorling.SqlExec.mapper.commands;

namespace Sorling.sqlexecCodeGen.project.cmds;
public record ProjectStoreIsInstalledCmd() : ScriptResourceCommand
{
   protected override string SqlResourceText => "Sorling.sqlexecCodeGen.project.sqlscripts.isinstalled.sql";

   protected override Type AssemblyType => typeof(ProjectStoreIsInstalledCmd);
}
