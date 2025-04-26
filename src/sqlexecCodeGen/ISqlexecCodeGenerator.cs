using Sorling.SqlExecMeta.objects.storedprocedures;

namespace Sorling.sqlexecCodeGen;

public interface ISqlexecCodeGenerator
{
   public ISqlexecCodeProjectSettings ProjectSettings { get; init; }

   public string GenerateHeader();

   public string GenerateOperation(IEnumerable<SqlStoredProcedureDefCmd.Result> sp);
}
