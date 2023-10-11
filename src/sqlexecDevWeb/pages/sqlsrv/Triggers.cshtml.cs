using Sorling.SqlConnAuthWeb.authentication;
using Sorling.sqlexecDevWeb.models.pagemodels;
using Sorling.SqlExecMeta;
using Sorling.SqlExecMeta.objects.triggers;

namespace Sorling.sqlexecDevWeb.pages.sqlsrv;

public class TriggersModel : DBItemPageModel
{
   public SqlTriggerItem? Trigger { get; private set; }

   public TriggersModel(ISqlConnAuthenticationService sqlAuth) : base(sqlAuth) { }

   protected override SqlGroupFlags GroupFlags => SqlGroupFlags.Triggers;

   protected override async Task<IEnumerable<ISqlItem>?> GetSqlListItemsAsync(string? schema)
      => await SqlMetadataProvider.GetTriggersAsync(schema);

   protected override async Task<IPrevNxtSqlItem?> GetPrevNxtSqlItemAsync(string schema, string name, string? schemaFolder, SqlGroupFlags? filterGroups)
      => await SqlMetadataProvider.GetTriggerPrevNxtAsync($"{schema}.{name}", schemaFolder);

   protected override async Task<ISqlItem?> GetSqlItemAsync(string schema, string name) {
      Trigger = await SqlMetadataProvider.GetTriggerAsync(schema, name);
      return Trigger;
   }

   protected override async Task<string?> GetDefinitionTextAsync(string schema, string name)
      => await Task.FromResult(Trigger?.Definition);
}
