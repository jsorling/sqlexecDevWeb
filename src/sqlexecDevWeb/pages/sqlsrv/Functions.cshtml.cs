using Sorling.SqlConnAuthWeb.authentication;
using Sorling.sqlexecDevWeb.extensions;
using Sorling.sqlexecDevWeb.models.pagemodels;
using Sorling.SqlExecMeta;
using Sorling.SqlExecMeta.objects.functions;

namespace Sorling.sqlexecDevWeb.pages.sqlsrv;

public class FunctionsModel : DBItemPageModel
{
   public IEnumerable<SqlFunctionDefCmd.Result>? Function { get; private set; }

   public FunctionsModel(ISqlConnAuthenticationService sqlAuth) : base(sqlAuth) { }

   protected override SqlGroupFlags GroupFlags => SqlGroupFlags.Functions;

   protected override async Task<IEnumerable<ISqlItem>?> GetSqlListItemsAsync()
      => await SqlMetadataProvider.GetSqlObjectsAsync(GroupFlags, DBSchema);

   protected async override Task<IPrevNxtSqlItem?> GetPrevNxtSqlItemAsync()
      => await SqlMetadataProvider.GetSqlObjectPrevNxtAsync(ItemFullName ?? "", GroupFlags.GetPageAction(), FilterSchema, FilterGroupFlags);

   protected async override Task<ISqlItem?> GetSqlItemAsync() {
      Function = await SqlMetadataProvider.GetSqlFunctionAsync(DBSchema!, ItemName!);
      return Function.FirstOrDefault();
   }

   protected async override Task<string?> GetDefinitionTextAsync() => await SqlMetadataProvider.GetSqlObjectTextAsync(ItemFullName!);
}
