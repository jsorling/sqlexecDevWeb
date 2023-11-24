using Microsoft.AspNetCore.Mvc.Rendering;
using Sorling.SqlExecMeta;

namespace Sorling.sqlexecDevWeb.extensions;

public static class ViewContextExtensions
{
   public static SqlGroupFlags? CurrentPageSqlGroup(this ViewContext vc) {
      string? currentpage = vc.RouteData.Values["page"]?.ToString()?.Split('/').LastOrDefault();
      return Enum.TryParse(currentpage, true, out SqlGroupFlags flags) ? flags : null;
   }
}
