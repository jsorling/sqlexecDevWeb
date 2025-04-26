using Sorling.SqlConnAuthWeb.authentication;
using Sorling.sqlexecDevWeb.models.pagemodels;
using Sorling.SqlExecMeta;
using Sorling.SqlExecMeta.constraints;

namespace Sorling.sqlexecDevWeb.pages.sqlsrv;

public class DefaultConstraintsModel(ISqlAuthService sqlAuth) : DBItemPageModel(sqlAuth)
{
   public SqlDefaultConstraintListItem? DefaultConstraintItem { get; private set; }

   protected override SqlGroupFlags GroupFlags => SqlGroupFlags.DefaultConstraints;

   protected override async Task<IEnumerable<ISqlItem>?> GetSqlListItemsAsync(string? schema)
      => await SqlMetadataProvider.GetDefaultContraintsAsync(schema);

   protected override async Task<IPrevNxtSqlItem?> GetPrevNxtSqlItemAsync(string schema, string name
      , string? schemaFolder, SqlGroupFlags? filterGroups)
      => await SqlMetadataProvider.GetSqlDefaultConstraintPrevNxtAsync($"{schema}.{name}", schemaFolder);

   protected override async Task<ISqlItem?> GetSqlItemAsync(string schema, string name) {
      DefaultConstraintItem = await SqlMetadataProvider.GetSqlDefaultConstraintAsync(schema, name);
      return DefaultConstraintItem;
   }

   protected override Task<string?> GetDefinitionTextAsync(string schema, string name)
      => Task.FromResult(DefaultConstraintItem?.Definition);
}
