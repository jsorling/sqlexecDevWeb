using Sorling.sqlexecCodeGen.helpers;
using Sorling.SqlExecMeta.objects.storedprocedures;

namespace Sorling.sqlexecCodeGen.csharp.sqlconext;

public class SqlConnExtensionStyle : ISqlexecCodeGenerator
{
   public ISqlexecCodeProjectSettings ProjectSettings { get; init; }

   public SqlConnExtensionStyle(ISqlexecCodeProjectSettings projectSettings) {
      ArgumentNullException.ThrowIfNull(projectSettings, nameof(projectSettings));
      ProjectSettings = projectSettings;
   }

   public string GenerateHeader() {
      string tmpl = IOHelper.LoadResourceString($"{CSharpConsts.RESOURCEPATH}SqlConnectionExtensionTemplate.cs");
      tmpl = tmpl.Replace("SqlConnectionExtensionTemplateNS", ProjectSettings.ProjectNamespace);
      tmpl = tmpl.Replace("SqlConnectionExtensionTemplateCS", ProjectSettings.ClassName);
      tmpl = tmpl.Replace("SqlOperationsOP", ProjectSettings.OperationClassName);
      tmpl = tmpl.Replace("DBExtensionName", ProjectSettings.ProjectName);

      return tmpl;
   }

   public string GenerateOperation(IEnumerable<SqlStoredProcedureDefCmd.Result> sp) => throw new NotImplementedException();
}
