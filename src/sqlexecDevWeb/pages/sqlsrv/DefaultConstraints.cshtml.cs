using Sorling.SqlConnAuthWeb.authentication;
using Sorling.sqlexecDevWeb.models.pagemodels;
using Sorling.SqlExecMeta;
using Sorling.SqlExecMeta.constraints;

namespace Sorling.sqlexecDevWeb.pages.sqlsrv;

public class DefaultConstraintsModel : DBItemPageModel
{
   public DefaultConstraintsModel(ISqlConnAuthenticationService sqlAuth) : base(sqlAuth) {
   }

   public SqlDefaultConstraintListItem? DefaultConstraintItem { get; private set; }

   protected override SqlGroupFlags GroupFlags => SqlGroupFlags.DefaultConstraints;

   protected override async Task<IEnumerable<ISqlItem>?> GetSqlListItemsAsync()
      => await SqlMetadataProvider.GetDefaultContraintsAsync(FilterSchema);

   protected override async Task<IPrevNxtSqlItem?> GetPrevNxtSqlItemAsync()
      => await SqlMetadataProvider.GetSqlDefaultConstraintPrevNxtAsync(ItemFullName!, FilterSchema);

   protected override async Task<ISqlItem?> GetSqlItemAsync() {
      DefaultConstraintItem = await SqlMetadataProvider.GetSqlDefaultConstraintAsync(DBSchema!, ItemName!);
      return DefaultConstraintItem;
   }

   protected override Task<string?> GetDefinitionTextAsync() => Task.FromResult(DefaultConstraintItem?.Definition);
}
