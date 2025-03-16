using System.Data.SqlClient;

namespace SqlConnectionExtensionTemplateNS;

public static class SqlConnectionExtensionTemplateCS
{
   public static SqlOperationsOP DBExtensionName(this SqlConnection sqlConnection) => new(sqlConnection);
}

public partial class SqlOperationsOP(SqlConnection sqlConnection)
{
   private readonly SqlConnection _sqlconnection = sqlConnection ?? throw new ArgumentNullException(nameof(sqlConnection));
}

public partial class SqlOperationsOP
{
   public string AScalarProcedure(string parameter) {
      using SqlCommand sqlcommand = _sqlconnection.CreateCommand();
      sqlcommand.CommandText = "dbo.something";
      sqlcommand.CommandType = System.Data.CommandType.StoredProcedure;
      
      _ = sqlcommand.Parameters.AddWithValue("@par", parameter);

      return "";
   }
}
