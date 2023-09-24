using Sorling.SqlConnAuthWeb.authentication;
using Sorling.sqlexecDevWeb.models.pagemodels;
using Sorling.SqlExecMeta;
using Sorling.SqlExecMeta.types;

namespace Sorling.sqlexecDevWeb.pages.sqlsrv;

public class TypesModel : DBItemPageModel
{
   public TypesModel(ISqlConnAuthenticationService sqlAuth) : base(sqlAuth) {
   }

   public IEnumerable<SqlTypeListItem>? TypeItems { get; private set; }

   protected override async Task<IEnumerable<ISqlItem>?> GetSqlListItemsAsync()
      => await SqlMetadataProvider.GetSqlTypesAsync(FilterSchema);

   protected override SqlGroupFlags GroupFlags => SqlGroupFlags.Types;

   public SqlTypeListItem? TypeItem { get; private set; }

   protected override async Task<IPrevNxtSqlItem?> GetPrevNxtSqlItemAsync()
      => await SqlMetadataProvider.GetSqlTypePrevNxtAsync(ItemFullName!, FilterSchema);

   protected async override Task<ISqlItem?> GetSqlItemAsync() {
      TypeItem = await SqlMetadataProvider.GetSqlTypeAsync(DBSchema!, ItemName!);
      return TypeItem;
   }
}
