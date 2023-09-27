using Sorling.SqlConnAuthWeb.authentication;
using Sorling.sqlexecDevWeb.extensions;
using Sorling.sqlexecDevWeb.models.pagemodels;
using Sorling.SqlExecMeta;
using Sorling.SqlExecMeta.objects;

namespace Sorling.sqlexecDevWeb.pages.sqlsrv;

public class ObjectsModel : DBItemPageModel
{
   public ObjectsModel(ISqlConnAuthenticationService sqlAuth) : base(sqlAuth) { }

   public IEnumerable<SqlObjectListItem>? Objects { get; private set; }

   protected override SqlGroupFlags GroupFlags => SqlGroupFlags.Objects;

   protected override async Task<IEnumerable<ISqlItem>?> GetSqlListItemsAsync()
      => await SqlMetadataProvider.GetSqlObjectsAsync(GroupFlags, DBSchema);

   protected override async Task<IPrevNxtSqlItem?> GetPrevNxtSqlItemAsync()
      => await SqlMetadataProvider.GetSqlObjectPrevNxtAsync(ItemFullName ?? "", GroupFlags.GetPageAction(), FilterSchema, FilterGroupFlags);

   protected override async Task<ISqlItem?> GetSqlItemAsync() => await Task.FromResult<ISqlItem?>(null);
}
