using System.Data.SqlClient;

namespace Test.CodeGenTests;

public static class CodeGenTest
{
   public static CodeGenTestOperation CodeGenTests(this SqlConnection sqlConnection) => new(sqlConnection);
}

public partial class CodeGenTestOperation(SqlConnection sqlConnection)
{
   private readonly SqlConnection _sqlconnection = sqlConnection ?? throw new ArgumentNullException(nameof(sqlConnection));
}

public partial class CodeGenTestOperation
{
   public string AScalarProcedure(string parameter) {
      using SqlCommand sqlcommand = _sqlconnection.CreateCommand();
      sqlcommand.CommandText = "dbo.something";
      sqlcommand.CommandType = System.Data.CommandType.StoredProcedure;
      
      _ = sqlcommand.Parameters.AddWithValue("@par", parameter);

      return "";
   }
}
