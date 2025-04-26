namespace Sorling.sqlexecCodeGen;

public interface ISqlexecCodeProjectSettings
{
   public string ProjectName { get; init; }

   public string ProjectNamespace { get; init; }

   public string OperationClassName { get; init; }

   public string ClassName { get; init; }
}
