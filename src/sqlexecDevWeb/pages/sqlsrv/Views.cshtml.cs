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

   protected override async Task<IEnumerable<ISqlItem>?> GetSqlListItemsAsync()
      => await SqlMetadataProvider.GetSqlObjectsAsync(GroupFlags, DBSchema);

   protected override async Task<IPrevNxtSqlItem?> GetPrevNxtSqlItemAsync()
      => await SqlMetadataProvider.GetSqlObjectPrevNxtAsync(ItemFullName ?? "", GroupFlags.GetPageAction(), FilterSchema, FilterGroupFlags);

   protected override async Task<ISqlItem?> GetSqlItemAsync() {
      View = await SqlMetadataProvider.GetSqlViewAsync(DBSchema!, ItemName!);
      return View.FirstOrDefault();
   }

   protected override async Task<string?> GetDefinitionTextAsync() => await SqlMetadataProvider.GetSqlObjectTextAsync(ItemFullName!);
}
