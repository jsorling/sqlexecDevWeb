using Sorling.SqlConnAuthWeb.authentication;
using Sorling.sqlexecDevWeb.models.pagemodels;
using Sorling.SqlExecMeta;
using Sorling.SqlExecMeta.constraints;

namespace Sorling.sqlexecDevWeb.pages.sqlsrv;

public class CheckConstraintsModel(ISqlConnAuthenticationService sqlAuth) : DBItemPageModel(sqlAuth)
{
   public SqlCheckConstraintListItem? CheckConstraintItem { get; private set; }

   protected override SqlGroupFlags GroupFlags => SqlGroupFlags.CheckConstraints;

   protected override async Task<IEnumerable<ISqlItem>?> GetSqlListItemsAsync(string? schema)
      => await SqlMetadataProvider.GetCheckContraintsAsync(schema);

   protected override async Task<IPrevNxtSqlItem?> GetPrevNxtSqlItemAsync(string schema, string name, string? schemaFolder, SqlGroupFlags? filterGroups)
      => await SqlMetadataProvider.GetSqlCheckConstraintPrevNxtAsync($"{schema}.{name}", schemaFolder);

   protected override async Task<ISqlItem?> GetSqlItemAsync(string schema, string name) {
      CheckConstraintItem = await SqlMetadataProvider.GetSqlCheckConstraintAsync(schema, name);
      return CheckConstraintItem;
   }

   protected override Task<string?> GetDefinitionTextAsync(string schema, string name) => Task.FromResult(CheckConstraintItem?.Definition);
}
