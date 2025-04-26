using Sorling.SqlExecMeta.objects.tabletypes;

namespace Sorling.sqlexecDevWeb.extensions.sqlobjects;

public static class TableTypeDefExtensions
{
   public static string GetPropertyString(this SqlTableTypeDefCmd.Result tableDefCmdRes) {
      List<string> list = new();

      if (tableDefCmdRes.IsIdentity)
      {
         list.Add("Identity");
      }

      if (tableDefCmdRes.IsNullable)
      {
         list.Add("Nullable");
      }

      if (tableDefCmdRes.IsComputed)
      {
         list.Add("Computed");
      }

      if (tableDefCmdRes.IsRowGuid)
      {
         list.Add("RowGuid");
      }

      return string.Join(", ", list);
   }
}
