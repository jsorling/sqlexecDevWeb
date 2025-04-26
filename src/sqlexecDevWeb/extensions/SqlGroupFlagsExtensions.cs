using Sorling.SqlExecMeta;

namespace Sorling.sqlexecDevWeb.extensions;

public static class SqlGroupFlagsExtensions
{
   public static string GetPageAction(this SqlGroupFlags sqlGroupFlags) => sqlGroupFlags.ToString().ToLower();

   public static string GetGroupItemName(this SqlGroupFlags sqlGroupFlags) => sqlGroupFlags switch {
      SqlGroupFlags.Tables => "Table",
      SqlGroupFlags.Views => "View",
      SqlGroupFlags.StoredProcedures => "Stored Procedure",
      SqlGroupFlags.Functions => "Function",
      SqlGroupFlags.TableTypes => "Table Type",
      SqlGroupFlags.ResultSet => "Result Set",
      SqlGroupFlags.Types => "Type",
      SqlGroupFlags.DefaultConstraints => "Default Constraint",
      SqlGroupFlags.CheckConstraints => "Check Constraint",
      SqlGroupFlags.Objects => "Object",
      SqlGroupFlags.DbRoles => "Database Role",
      SqlGroupFlags.AppRoles => "Application Role",
      SqlGroupFlags.UserCertificates => "User Cerificate",
      SqlGroupFlags.ExtUsersAzureAD => "Azure AD User",
      SqlGroupFlags.WinGroups => "Windows Group",
      SqlGroupFlags.UserAsymetricKeys => "User Asymetric Key",
      SqlGroupFlags.SqlUsers => "Sql User",
      SqlGroupFlags.WinUsers => "Windows User",
      SqlGroupFlags.ExtGroupsAzureAD => "Azure AD Group",
      SqlGroupFlags.Triggers => "Trigger",
      _ => "Unknown",
   };

   public static bool In(this SqlGroupFlags sqlGroupFlags, params SqlGroupFlags[] flags) {
      foreach (SqlGroupFlags flag in flags)
      {
         if (flag == sqlGroupFlags)
            return true;
      }

      return false;
   }
}
