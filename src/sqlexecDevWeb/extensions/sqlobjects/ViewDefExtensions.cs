using Sorling.SqlExecMeta.objects.views;

namespace Sorling.sqlexecDevWeb.extensions.sqlobjects;

public static class ViewDefExtensions
{
   public static string GetPropertyString(this SqlViewDefCmd.Result tableDefCmdRes) {
      List<string> list = new();

      if (tableDefCmdRes.HasDefault ?? false) {
         list.Add("Has default");
      }

      if (tableDefCmdRes.IsNullable) {
         list.Add("Nullable");
      }

      return string.Join(", ", list);
   }
}
