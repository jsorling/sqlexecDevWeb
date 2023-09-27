using Sorling.SqlConnAuthWeb.authentication;
using Sorling.sqlexecDevWeb.models.pagemodels;
using Sorling.SqlExecMeta;
using Sorling.SqlExecMeta.constraints;

namespace Sorling.sqlexecDevWeb.pages.sqlsrv;

public class CheckConstraintsModel : DBItemPageModel
{
   public CheckConstraintsModel(ISqlConnAuthenticationService sqlAuth) : base(sqlAuth) {
   }

   public SqlCheckConstraintListItem? CheckConstraintItem { get; private set; }

   protected override SqlGroupFlags GroupFlags => SqlGroupFlags.CheckConstraints;

   protected override async Task<IEnumerable<ISqlItem>?> GetSqlListItemsAsync()
      => await SqlMetadataProvider.GetCheckContraintsAsync(FilterSchema);

   protected override async Task<IPrevNxtSqlItem?> GetPrevNxtSqlItemAsync()
      => await SqlMetadataProvider.GetSqlCheckConstraintPrevNxtAsync(ItemFullName!, FilterSchema);

   protected override async Task<ISqlItem?> GetSqlItemAsync() {
      CheckConstraintItem = await SqlMetadataProvider.GetSqlCheckConstraintAsync(DBSchema!, ItemName!);
      return CheckConstraintItem;
   }

   protected override Task<string?> GetDefinitionTextAsync() => Task.FromResult(CheckConstraintItem?.Definition);
}
