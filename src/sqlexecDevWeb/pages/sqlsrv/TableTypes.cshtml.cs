using Sorling.SqlConnAuthWeb.authentication;
using Sorling.sqlexecDevWeb.models.pagemodels;
using Sorling.SqlExecMeta;
using Sorling.SqlExecMeta.objects.tabletypes;

namespace Sorling.sqlexecDevWeb.pages.sqlsrv;

public class TableTypesModel(ISqlConnAuthenticationService sqlAuth) : DBItemPageModel(sqlAuth)
{
   public IEnumerable<SqlTableTypeDefCmd.Result>? TableType { get; private set; }

   protected override SqlGroupFlags GroupFlags => SqlGroupFlags.TableTypes;

   protected override async Task<IEnumerable<ISqlItem>?> GetSqlListItemsAsync(string? schema)
      => await SqlMetadataProvider.GetSqlObjectsAsync(GroupFlags, schema);

   protected override async Task<IPrevNxtSqlItem?> GetPrevNxtSqlItemAsync(string schema, string name, string? schemaFolder, SqlGroupFlags? filterGroups)
      => await SqlMetadataProvider.GetSqlTypePrevNxtAsync($"{schema}.{name}", schemaFolder);

   protected override async Task<ISqlItem?> GetSqlItemAsync(string schema, string name) {
      TableType = await SqlMetadataProvider.GetSqlTableTypeAsync($"{schema}.{name}");
      return TableType.FirstOrDefault();
   }
}
