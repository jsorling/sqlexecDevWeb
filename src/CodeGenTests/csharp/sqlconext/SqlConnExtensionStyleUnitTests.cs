using Sorling.sqlexecCodeGen.csharp.sqlconext;
using Sorling.sqlexecCodeGen.helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenTests.csharp.sqlconext;

[TestClass]
public class SqlConnExtensionStyleUnitTests
{
   [TestMethod]
   public void GenerateHeader() {
      SqlConnExtensionStyle sqlconnextensionstyle = new(TestsInitialize.CodeProjectSettings);
      IOHelper.SaveStringToFile(".\\..\\..\\..\\..\\CodeOutput\\CodeGenTest.cs", sqlconnextensionstyle.GenerateHeader());
   }
}
