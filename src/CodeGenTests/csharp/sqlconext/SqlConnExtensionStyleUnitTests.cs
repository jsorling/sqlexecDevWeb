using Sorling.sqlexecCodeGen.csharp.sqlconext;
using Sorling.sqlexecCodeGen.helpers;

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
