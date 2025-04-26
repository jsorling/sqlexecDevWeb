using Sorling.sqlexecCodeGen.csharp;
using Sorling.sqlexecCodeGen.helpers;

namespace CodeGenTests.helpers;

[TestClass]
public class IOHelperUtnitTests
{
   [TestMethod]
   public void LoadResourceString()
      => Assert.IsTrue(IOHelper.LoadResourceString($"{CSharpConsts.RESOURCEPATH}SqlConnectionExtensionTemplate.cs").Length > 0);
}
