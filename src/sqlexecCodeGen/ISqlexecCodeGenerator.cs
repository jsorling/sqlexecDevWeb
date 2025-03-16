using Sorling.SqlExecMeta.objects.storedprocedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Sorling.sqlexecCodeGen;

public interface ISqlexecCodeGenerator
{
   public ISqlexecCodeProjectSettings ProjectSettings { get; init; }

   public string GenerateHeader();

   public string GenerateOperation(IEnumerable<SqlStoredProcedureDefCmd.Result> sp);
}
