using Microsoft.AspNetCore.Mvc.Filters;
using Sorling.SqlExecMeta;

namespace Sorling.sqlexecDevWeb.filters;

public class SchemaNObjectFilter : IAsyncPageFilter
{
   private readonly string _rd1;

   private readonly string _rd2;

   private readonly string _rd3;

   public SchemaNObjectFilter(string routeDataKey1st = "schema", string routeDataKey2nd = "obj", string routeDataKey3rd = "filter") {
      _rd1 = routeDataKey1st;
      _rd2 = routeDataKey2nd;
      _rd3 = routeDataKey3rd;
   }

   public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
      => await next.Invoke();

   public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context) {
      string parschema = context.RouteData.Values[_rd1]?.ToString() ?? string.Empty;
      string parobj = context.RouteData.Values[_rd2]?.ToString() ?? string.Empty;

      if (parschema.Contains('.')) {
         // first (parschema) is object
         context.RouteData.Values[RouteDataKeysConsts.REQSQLOBJECTKEY] = parschema;
         if (long.TryParse(parobj, out long f)) {
            // second (parobj) is filter
            context.RouteData.Values[RouteDataKeysConsts.REQSQLFILTERKEY] = (SqlGroupFlags)f;
         }
      }
      else if (parschema != string.Empty) {
         // first (parschema) is schema
         context.RouteData.Values[RouteDataKeysConsts.REQSQLSCHEMAKEY] = parschema;
         string parfilter = context.RouteData.Values[_rd3]?.ToString() ?? string.Empty;
         if (parobj.Contains('.')) {
            // second (parobject) is object
            context.RouteData.Values[RouteDataKeysConsts.REQSQLOBJECTKEY] = parobj;
            if (long.TryParse(parfilter, out long f)) {
               // third (parfilter) is filter
               context.RouteData.Values[RouteDataKeysConsts.REQSQLFILTERKEY] = (SqlGroupFlags)f;
            }
         }
         else if (long.TryParse(parobj, out long f)) {
            // second (parobj) is filter
            context.RouteData.Values[RouteDataKeysConsts.REQSQLFILTERKEY] = (SqlGroupFlags)f;
         }
      }

      return Task.CompletedTask;
   }
}
