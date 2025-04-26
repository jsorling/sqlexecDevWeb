using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Sorling.SqlConnAuthWeb;
using Sorling.sqlexecDevWeb.extensions;
using Sorling.sqlexecDevWeb.extensions.sqlobjects;
using Sorling.SqlExecMeta;

namespace Sorling.sqlexecDevWeb.taghelpers;

//https://github.com/dotnet/aspnetcore/blob/main/src/Mvc/Mvc.TagHelpers/src/AnchorTagHelper.cs

[HtmlTargetElement("a", Attributes = _schemaAttributeName)]
[HtmlTargetElement("a", Attributes = _dbAttributeName)]
[HtmlTargetElement("a", Attributes = _projectAttributeName)]
[HtmlTargetElement("a", Attributes = _objectAttributeName)]
[HtmlTargetElement("a", Attributes = _pageAttributeName)]
[HtmlTargetElement("a", Attributes = _filterCurrentAttributeName)]
[HtmlTargetElement("a", Attributes = _filterKeepAttributeName)]
[HtmlTargetElement("a", Attributes = _prvItemAttributeName)]
[HtmlTargetElement("a", Attributes = _nxtItemAttributeName)]
[HtmlTargetElement("a", Attributes = _pageClassActiveAttributeName)]
[HtmlTargetElement("a", Attributes = _pageByNameAttributeName)]
public class DbLinkTagHelper(IHtmlGenerator htmlGenerator) : TagHelper
{
   private const string _schemaAttributeName = "sed-schema";

   private const string _dbAttributeName = "sed-db";

   private const string _projectAttributeName = "sed-project";

   private const string _objectAttributeName = "sed-sqlitem";

   private const string _pageAttributeName = "sed-page";

   private const string _filterCurrentAttributeName = "sed-filter-current-page";

   private const string _filterKeepAttributeName = "sed-filter-keep";

   private const string _prvItemAttributeName = "sed-prv-item";

   private const string _nxtItemAttributeName = "sed-nxt-item";

   private const string _pageClassActiveAttributeName = "sed-class-page-active";

   private const string _pageByNameAttributeName = "sed-page-byname";

   [HtmlAttributeName(_schemaAttributeName)]
   public string? Schema { get; set; }

   [HtmlAttributeName(_dbAttributeName)]
   public string? DBName { get; set; }

   [HtmlAttributeName(_projectAttributeName)]
   public string? Project { get; set; }

   [HtmlAttributeName(_objectAttributeName)]
   public ISqlItem? SqlItem { get; set; }

   [HtmlAttributeName(_pageAttributeName)]
   public SqlGroupFlags? Page { get; set; }

   [HtmlAttributeName(_pageClassActiveAttributeName)]
   public string? PageClassActive { get; set; }

   [HtmlAttributeName(_filterCurrentAttributeName)]
   public bool FilterCurrentPage { get; set; } = false;

   [HtmlAttributeName(_filterKeepAttributeName)]
   public bool FilterKeep { get; set; } = false;

   [HtmlAttributeName(_prvItemAttributeName)]
   public IPrevNxtSqlItem? PrvItem { get; set; }

   [HtmlAttributeName(_nxtItemAttributeName)]
   public IPrevNxtSqlItem? NxtItem { get; set; }

   [HtmlAttributeName(_pageByNameAttributeName)]
   public string? PageByName { get; set; }

   [HtmlAttributeNotBound]
   [ViewContext]
   public ViewContext? ViewContext { get; set; }

   public override void Process(TagHelperContext context, TagHelperOutput output) {
      ArgumentNullException.ThrowIfNull(context);
      ArgumentNullException.ThrowIfNull(output);
      ArgumentNullException.ThrowIfNull(ViewContext);

      if ((PrvItem is not null && SqlItem is not null) || (NxtItem is not null && SqlItem is not null))
      {
         throw new InvalidOperationException(
            $"{_prvItemAttributeName} or {_nxtItemAttributeName} and {_objectAttributeName} can not be set at the same time in an <a>-tag");
      }

      if (PrvItem is not null && NxtItem is not null)
      {
         throw new InvalidOperationException($"Both {_nxtItemAttributeName} and {_prvItemAttributeName} can not be set in <a>-tag");

      }

      string db = DBName ?? ViewContext.RouteData.Values[RouteDataKeysConsts.URLROUTEPARAMDB]?.ToString()
         ?? throw new InvalidOperationException("Database name is not on route or defined in <a>-tag");

      string prj = Project ?? ViewContext.RouteData.Values[RouteDataKeysConsts.URLROUTEPARAMPROJECT]?.ToString()
         ?? RouteDataKeysConsts.DEFAULTEMPTYPROJECT;

      RouteValueDictionary routevalues = new() {
         { SqlAuthConsts.URLROUTEPARAMSRV, ViewContext.RouteData.Values[SqlAuthConsts.URLROUTEPARAMSRV] },
         { SqlAuthConsts.URLROUTEPARAMUSR, ViewContext.RouteData.Values[SqlAuthConsts.URLROUTEPARAMUSR] },
         { RouteDataKeysConsts.URLROUTEPARAMDB, db }, { RouteDataKeysConsts.URLROUTEPARAMPROJECT, prj } };

      string? outschema = Schema == "" ? null
         : Schema is not null
         ? Schema
         : ViewContext.RouteData.Values[RouteDataKeysConsts.REQSQLSCHEMAKEY]?.ToString();
      string? outobject = null;
      string? outfilter = null;

      string pg = PageByName ?? Page?.GetPageAction() ?? ViewContext.RouteData.Values["page"]?.ToString()
         ?? SqlGroupFlags.Objects.GetPageAction();

      if (SqlItem is not null)
      {
         outobject = SqlItem.FQItemName();
         pg = SqlItem.GroupFlag.GetPageAction();
      }

      if (FilterCurrentPage)
      {
         string? currentpage = ViewContext.RouteData.Values["page"]?.ToString()?.Split('/').LastOrDefault();
         if (Enum.TryParse(currentpage, true, out SqlGroupFlags flags))
         {
            outfilter = ((long)flags).ToString();
         }
      }
      else if (FilterKeep)
      {
         if (ViewContext.RouteData.Values[RouteDataKeysConsts.REQSQLFILTERKEY] is SqlGroupFlags flags)
         {
            outfilter = ((long)flags).ToString();
         }
      }

      if (PrvItem is not null && PrvItem.PreviousGroup.HasValue)
      {
         outobject = PrvItem.PreviousId;
         pg = PrvItem.PreviousGroup.Value.GetPageAction();
      }
      else if (NxtItem is not null && NxtItem.NextGroup.HasValue)
      {
         outobject = NxtItem.NextId;
         pg = NxtItem.NextGroup.Value.GetPageAction();
      }

      if (!string.IsNullOrEmpty(outschema))
      {
         routevalues.Add("schema", outschema);
         routevalues.Add("obj", outobject);
         if (outfilter != null)
         {
            routevalues.Add("filter", outfilter);
         }
      }
      else
      {
         routevalues.Add("schema", outobject);
         if (outfilter != null)
         {
            routevalues.Add("obj", outfilter);
         }
      }

      Dictionary<string, object>? htmlattributes = null;

      if (!string.IsNullOrEmpty(PageClassActive) && ViewContext.CurrentPageSqlGroup() == Page)
      {
         htmlattributes = new Dictionary<string, object>() { { "class", PageClassActive } };
      }

      ;

      TagBuilder tagbuilder = htmlGenerator.GeneratePageLink(
         ViewContext,
         linkText: string.Empty,
         pageName: pg,
         pageHandler: null,
         protocol: null,
         hostname: null,
         fragment: null,
         routeValues: routevalues,
         htmlAttributes: htmlattributes);

      output.MergeAttributes(tagbuilder);
   }
}
