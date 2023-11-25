using Sorling.SqlConnAuthWeb.authentication;
using Sorling.sqlexecDevWeb.extensions;
using Sorling.sqlexecDevWeb.models.pagemodels;
using Sorling.SqlExecMeta;
using Sorling.SqlExecMeta.constraints;
using Sorling.SqlExecMeta.objects.tables;

namespace Sorling.sqlexecDevWeb.pages.sqlsrv;

public class TablesModel(ISqlConnAuthenticationService sqlAuth) : DBItemPageModel(sqlAuth)
{
   public IEnumerable<TableDefCmd.Result>? Table { get; private set; }

   public IEnumerable<SqlCheckConstraintListItem>? CheckConstraints { get; private set; }

   public IEnumerable<SqlDefaultConstraintListItem>? DefaultConstraints { get; private set; }

   protected override SqlGroupFlags GroupFlags => SqlGroupFlags.Tables;

   protected override async Task<IEnumerable<ISqlItem>?> GetSqlListItemsAsync(string? schema)
      => await SqlMetadataProvider.GetSqlObjectsAsync(GroupFlags, schema);

   protected override async Task<IPrevNxtSqlItem?> GetPrevNxtSqlItemAsync(string schema, string name, string? schemaFolder, SqlGroupFlags? filterGroups)
      => await SqlMetadataProvider.GetSqlObjectPrevNxtAsync($"{schema}.{name}", GroupFlags, schemaFolder
         , filterGroups ?? SqlGroupFlags.Objects);

   protected override async Task<ISqlItem?> GetSqlItemAsync(string schema, string name) {
      Table = await SqlMetadataProvider.GetSqlTableAsync(schema, name);
      CheckConstraints = await SqlMetadataProvider.GetCheckContraintsForObjectAsync($"{schema}.{name}");
      DefaultConstraints = await SqlMetadataProvider.GetDefaultContraintsForObjectAsync($"{schema}.{name}");
      return Table.FirstOrDefault();
   }
}
