using Sorling.SqlConnAuthWeb.authentication;
using Sorling.sqlexecDevWeb.extensions;
using Sorling.sqlexecDevWeb.models.pagemodels;
using Sorling.SqlExecMeta;
using Sorling.SqlExecMeta.objects.views;

namespace Sorling.sqlexecDevWeb.pages.sqlsrv;

public class ViewsModel : DBItemPageModel
{
   public IEnumerable<SqlViewDefCmd.Result>? View { get; private set; }

   public ViewsModel(ISqlConnAuthenticationService sqlAuth) : base(sqlAuth) { }

   protected override SqlGroupFlags GroupFlags => SqlGroupFlags.Views;

   protected override async Task<IEnumerable<ISqlItem>?> GetSqlListItemsAsync(string? schema)
      => await SqlMetadataProvider.GetSqlObjectsAsync(GroupFlags, schema);

   protected override async Task<IPrevNxtSqlItem?> GetPrevNxtSqlItemAsync(string schema, string name, string? schemaFolder, SqlGroupFlags? filterGroups)
      => await SqlMetadataProvider.GetSqlObjectPrevNxtAsync($"{schema}.{name}", GroupFlags.GetPageAction(), schemaFolder
         , filterGroups ?? SqlGroupFlags.Objects);

   protected override async Task<ISqlItem?> GetSqlItemAsync(string schema, string name) {
      View = await SqlMetadataProvider.GetSqlViewAsync(schema, name);
      return View.FirstOrDefault();
   }

   protected override async Task<string?> GetDefinitionTextAsync(string schema, string name)
      => await SqlMetadataProvider.GetSqlObjectTextAsync($"{schema}.{name}");
}
