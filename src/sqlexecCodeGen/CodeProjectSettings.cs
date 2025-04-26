namespace Sorling.sqlexecCodeGen;

public class CodeProjectSettings : ISqlexecCodeProjectSettings
{
   public string ProjectName { get; init; }

   public string ProjectNamespace { get; init; }

   public string OperationClassName { get; init; }

   public string ClassName { get; init; }

   public CodeProjectSettings(string projectName, string projectNamespace, string operationClassName, string className)
      => (ProjectName, ProjectNamespace, OperationClassName, ClassName)
         = (projectName, projectNamespace, operationClassName, className);
}
