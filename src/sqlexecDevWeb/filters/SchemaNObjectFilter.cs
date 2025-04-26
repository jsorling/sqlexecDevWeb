using Microsoft.AspNetCore.Mvc.Filters;
using Sorling.SqlExecMeta;

namespace Sorling.sqlexecDevWeb.filters;

[AttributeUsage(AttributeTargets.Class)]
public class SchemaNObjectFilterAttribute : Attribute, IAsyncPageFilter
{
   private readonly string _rd1 = "schema";

   private readonly string _rd2 = "obj";

   private readonly string _rd3 = "filter";

   public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
      => await next.Invoke();

   public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context) {
      string parschema = context.RouteData.Values[_rd1]?.ToString() ?? string.Empty;
      string parobj = context.RouteData.Values[_rd2]?.ToString() ?? string.Empty;

      if (parschema.Contains('.'))
      {
         // first (parschema) is object
         context.RouteData.Values[RouteDataKeysConsts.REQSQLOBJECTKEY] = parschema;
         if (long.TryParse(parobj, out long f))
         {
            // second (parobj) is filter
            context.RouteData.Values[RouteDataKeysConsts.REQSQLFILTERKEY] = (SqlGroupFlags)f;
         }
      }
      else if (parschema != string.Empty)
      {
         // first (parschema) is schema
         context.RouteData.Values[RouteDataKeysConsts.REQSQLSCHEMAKEY] = parschema;
         string parfilter = context.RouteData.Values[_rd3]?.ToString() ?? string.Empty;
         if (parobj.Contains('.'))
         {
            // second (parobject) is object
            context.RouteData.Values[RouteDataKeysConsts.REQSQLOBJECTKEY] = parobj;
            if (long.TryParse(parfilter, out long f))
            {
               // third (parfilter) is filter
               context.RouteData.Values[RouteDataKeysConsts.REQSQLFILTERKEY] = (SqlGroupFlags)f;
            }
         }
         else if (long.TryParse(parobj, out long f))
         {
            // second (parobj) is filter
            context.RouteData.Values[RouteDataKeysConsts.REQSQLFILTERKEY] = (SqlGroupFlags)f;
         }
      }

      string? page = context.RouteData.Values["page"]?.ToString()?.Split('/').LastOrDefault();
      if (page != null && Enum.TryParse(page, true, out SqlGroupFlags flags))
      {
         context.RouteData.Values[RouteDataKeysConsts.REQSQLPAGEGROUPKEY] = flags;
      }

      return Task.CompletedTask;
   }
}
