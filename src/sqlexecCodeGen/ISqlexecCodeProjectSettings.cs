using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorling.sqlexecCodeGen;

public interface ISqlexecCodeProjectSettings
{
   public string ProjectName { get; init; }

   public string ProjectNamespace { get; init; }

   public string OperationClassName { get; init; }

   public string ClassName { get; init; }
}
