using Sorling.SqlConnAuthWeb.authentication;
using Sorling.sqlexecDevWeb.models.pagemodels;
using Sorling.SqlExecMeta;
using Sorling.SqlExecMeta.types;

namespace Sorling.sqlexecDevWeb.pages.sqlsrv;

public class TypesModel(ISqlConnAuthenticationService sqlAuth) : DBItemPageModel(sqlAuth)
{
   public IEnumerable<SqlTypeListItem>? TypeItems { get; private set; }

   protected override async Task<IEnumerable<ISqlItem>?> GetSqlListItemsAsync(string? schema)
      => await SqlMetadataProvider.GetSqlTypesAsync(schema);

   protected override SqlGroupFlags GroupFlags => SqlGroupFlags.Types;

   public SqlTypeListItem? TypeItem { get; private set; }

   protected override async Task<IPrevNxtSqlItem?> GetPrevNxtSqlItemAsync(string schema, string name, string? schemaFolder, SqlGroupFlags? filterGroups)
      => await SqlMetadataProvider.GetSqlTypePrevNxtAsync($"{schema}.{name}", schemaFolder);

   protected override async Task<ISqlItem?> GetSqlItemAsync(string schema, string name) {
      TypeItem = await SqlMetadataProvider.GetSqlTypeAsync(schema, name);
      return TypeItem;
   }
}
