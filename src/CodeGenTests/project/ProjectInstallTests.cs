using Sorling.sqlexecCodeGen.project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
