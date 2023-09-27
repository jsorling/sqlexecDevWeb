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

   protected override async Task<IEnumerable<ISqlItem>?> GetSqlListItemsAsync()
      => await SqlMetadataProvider.GetTriggersAsync(DBSchema);

   protected override async Task<IPrevNxtSqlItem?> GetPrevNxtSqlItemAsync()
      => await SqlMetadataProvider.GetTriggerPrevNxtAsync(ItemFullName ?? "", FilterSchema);

   protected override async Task<ISqlItem?> GetSqlItemAsync() {
      Trigger = await SqlMetadataProvider.GetTriggerAsync(DBSchema!, ItemName!);
      return Trigger;
   }

   protected override async Task<string?> GetDefinitionTextAsync() => await Task.FromResult(Trigger?.Definition);
}
