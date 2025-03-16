using Sorling.SqlExec.mapper.commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorling.sqlexecCodeGen.project.cmds;
public record ProjectStoreIsInstalledCmd() : ScriptResourceCommand
{
   protected override string SqlResourceText => "Sorling.sqlexecCodeGen.project.sqlscripts.isinstalled.sql";

   protected override Type AssemblyType => typeof(ProjectStoreIsInstalledCmd);
}
