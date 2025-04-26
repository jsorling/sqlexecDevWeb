using Sorling.sqlexecCodeGen.project;

namespace CodeGenTests.project;

[TestClass]
public class ProjectInstallTests
{
   [TestMethod]
   public void IsInstalled() {
      SqlExecProject sqlproj = new(TestsInitialize.SQLExecRunner);
      bool isinstalled = sqlproj.ProjectStoreIsInstalledAsync().Result;
      Assert.IsFalse(isinstalled);
   }
}
