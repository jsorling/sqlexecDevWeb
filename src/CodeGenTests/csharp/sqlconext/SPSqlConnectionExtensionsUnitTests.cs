using Sorling.sqlexecCodeGen.csharp.sqlconext;
using Sorling.SqlExecMeta;
using Sorling.SqlExecMeta.objects.storedprocedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
