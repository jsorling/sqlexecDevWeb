using Sorling.SqlConnAuthWeb.authentication;
using Sorling.sqlexecDevWeb.models.pagemodels;
using Sorling.SqlExecMeta;
using Sorling.SqlExecMeta.objects.tabletypes;

namespace Sorling.sqlexecDevWeb.pages.sqlsrv;

public class TableTypesModel : DBItemPageModel
{
   public TableTypesModel(ISqlConnAuthenticationService sqlAuth) : base(sqlAuth) { }

   public IEnumerable<SqlTableTypeDefCmd.Result>? TableType { get; private set; }

   protected override SqlGroupFlags GroupFlags => SqlGroupFlags.TableTypes;

   protected override async Task<IEnumerable<ISqlItem>?> GetSqlListItemsAsync()
      => await SqlMetadataProvider.GetSqlObjectsAsync(GroupFlags, DBSchema);

   protected override async Task<IPrevNxtSqlItem?> GetPrevNxtSqlItemAsync()
      => await SqlMetadataProvider.GetSqlTypePrevNxtAsync(ItemFullName!, FilterSchema);

   protected override async Task<ISqlItem?> GetSqlItemAsync() {
      TableType = await SqlMetadataProvider.GetSqlTableTypeAsync($"{DBSchema}.{ItemName}");
      return TableType.FirstOrDefault();
   }
}
