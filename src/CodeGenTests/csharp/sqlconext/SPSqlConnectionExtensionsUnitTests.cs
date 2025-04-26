using Sorling.sqlexecCodeGen.csharp.sqlconext;
using Sorling.SqlExecMeta.objects.storedprocedures;

namespace CodeGenTests.csharp.sqlconext;

[TestClass]
public class SPSqlConnectionExtensionsUnitTests
{
   [TestMethod]
   public void WriteTemplate()
      => SPSqlConnectionExtensions.WriteTemplateToFile(".\\..\\..\\..\\..\\CodeOutput\\SPSqlConnectionExtensions.cs");

   [TestMethod]
   public void ReturnThreeNoPar() {
      IEnumerable<SqlStoredProcedureDefCmd.Result> sp
         = TestsInitialize.SqlMetadataProvider.GetSqlStoredProcedureAsync("sqlexeccodegentests", "ReturnThreeNoPar").Result;
      string s = sp.GetCSharpConnectionStyleCode();
   }
}
