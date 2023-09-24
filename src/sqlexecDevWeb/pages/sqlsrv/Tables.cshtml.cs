using Sorling.SqlConnAuthWeb.authentication;
using Sorling.sqlexecDevWeb.extensions;
using Sorling.sqlexecDevWeb.models.pagemodels;
using Sorling.SqlExecMeta;
using Sorling.SqlExecMeta.objects.tables;

namespace Sorling.sqlexecDevWeb.pages.sqlsrv;

public class TablesModel : DBItemPageModel
{
   public IEnumerable<TableDefCmd.Result>? Table { get; private set; }

   public TablesModel(ISqlConnAuthenticationService sqlAuth) : base(sqlAuth) { }

   protected override SqlGroupFlags GroupFlags => SqlGroupFlags.Tables;

   protected override async Task<IEnumerable<ISqlItem>?> GetSqlListItemsAsync()
      => await SqlMetadataProvider.GetSqlObjectsAsync(GroupFlags, DBSchema);

   protected async override Task<IPrevNxtSqlItem?> GetPrevNxtSqlItemAsync()
      => await SqlMetadataProvider.GetSqlObjectPrevNxtAsync(ItemFullName ?? "", GroupFlags.GetPageAction(), FilterSchema, FilterGroupFlags);

   protected async override Task<ISqlItem?> GetSqlItemAsync() {
      Table = await SqlMetadataProvider.GetSqlTableAsync(DBSchema!, ItemName!);
      return Table.FirstOrDefault();
   }
}
