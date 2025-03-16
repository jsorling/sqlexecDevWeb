using Sorling.sqlexecCodeGen.helpers;
using Sorling.SqlExecMeta.objects.storedprocedures;
using Sorling.SqlExecMeta.sql2code.csharp;
using System.Text;

namespace Sorling.sqlexecCodeGen.csharp.sqlconext;

public static class SPSqlConnectionExtensions
{
   public static string GetCSharpConnectionStyleCode(this IEnumerable<SqlStoredProcedureDefCmd.Result> sp) {
      Sql2CSharp csmapper = new();
      return new StringBuilder().Append("using Microsoft.Data.SqlClient;\n\nnamespace ns;\n\n")
         .Append($"public static class {csmapper.MapSql2ProperName(sp.First().DBName)}SqlConnectionExtension\n {{\n")
         .Append("  public static SqlOperations DBName(this SqlConnection sqlConnection) => new(sqlConnection);\n")
         .Append("}\n\npublic partial class SqlOperations\n{  \n")
         .Append("}\n").ToString();
   }

   public static void WriteTemplateToFile(string path) {
      string c = IOHelper.LoadResourceString($"{CSharpConsts.RESOURCEPATH}SqlConnectionExtensionTemplate.cs");
      IOHelper.SaveStringToFile(path, c);
   }
}
