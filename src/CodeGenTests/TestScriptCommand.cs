using Sorling.SqlExec.mapper.commands;

namespace CodeGenTests;

internal record TestScriptCommand : ScriptResourceCommand
{
   protected override string SqlResourceText => "CodeGenTests.TestObjects.sql";

   protected override Type AssemblyType => typeof(TestScriptCommand);
}
