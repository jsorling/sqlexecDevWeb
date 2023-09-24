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

   protected async override Task<IPrevNxtSqlItem?> GetPrevNxtSqlItemAsync()
      => await SqlMetadataProvider.GetSqlObjectPrevNxtAsync(ItemFullName ?? "", GroupFlags.GetPageAction(), FilterSchema, FilterGroupFlags);

   protected async override Task<ISqlItem?> GetSqlItemAsync() {
      View = await SqlMetadataProvider.GetSqlViewAsync(DBSchema!, ItemName!);
      return View.FirstOrDefault();
   }

   protected async override Task<string?> GetDefinitionTextAsync() => await SqlMetadataProvider.GetSqlObjectTextAsync(ItemFullName!);
}
