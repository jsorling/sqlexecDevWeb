using Sorling.SqlConnAuthWeb.authentication;
using Sorling.sqlexecDevWeb.models.pagemodels;
using Sorling.SqlExecMeta;
using Sorling.SqlExecMeta.objects;

namespace Sorling.sqlexecDevWeb.pages.sqlsrv;

public class ObjectsModel(ISqlAuthService sqlAuth) : DBItemPageModel(sqlAuth)
{
   public IEnumerable<SqlObjectListItem>? Objects { get; private set; }

   protected override SqlGroupFlags GroupFlags => SqlGroupFlags.Objects;

   protected override async Task<IEnumerable<ISqlItem>?> GetSqlListItemsAsync(string? schema)
      => await SqlMetadataProvider.GetSqlObjectsAsync(GroupFlags, schema);

   protected override async Task<IPrevNxtSqlItem?> GetPrevNxtSqlItemAsync(string schema, string name, string? schemaFolder, SqlGroupFlags? filterGroups)
      => await SqlMetadataProvider.GetSqlObjectPrevNxtAsync($"{schema}.{name}", GroupFlags, schemaFolder
         , filterGroups ?? SqlGroupFlags.Objects);

   protected override async Task<ISqlItem?> GetSqlItemAsync(string schema, string name) => await Task.FromResult<ISqlItem?>(null);
}
