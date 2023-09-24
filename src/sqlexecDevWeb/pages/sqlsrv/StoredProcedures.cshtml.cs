using Sorling.SqlConnAuthWeb.authentication;
using Sorling.sqlexecDevWeb.extensions;
using Sorling.sqlexecDevWeb.models.pagemodels;
using Sorling.SqlExecMeta;
using Sorling.SqlExecMeta.objects;
using Sorling.SqlExecMeta.objects.storedprocedures;

namespace Sorling.sqlexecDevWeb.pages.sqlsrv;

public class StoredProceduresModel : DBItemPageModel
{
   public IEnumerable<SqlStoredProcedureDefCmd.Result>? SP { get; private set; }

   public Dictionary<int, IEnumerable<SqlResultSetColumn>>? ResultSets { get; private set; }

   protected override SqlGroupFlags GroupFlags => SqlGroupFlags.StoredProcedures;

   public StoredProceduresModel(ISqlConnAuthenticationService sqlAuth) : base(sqlAuth) { }

   protected override async Task<IEnumerable<ISqlItem>?> GetSqlListItemsAsync()
      => await SqlMetadataProvider.GetSqlObjectsAsync(GroupFlags, DBSchema);

   public Exception? ResultSetError { get; private set; }

   protected async override Task<IPrevNxtSqlItem?> GetPrevNxtSqlItemAsync()
      => await SqlMetadataProvider.GetSqlObjectPrevNxtAsync(ItemFullName ?? "", GroupFlags.GetPageAction(), FilterSchema, FilterGroupFlags);

   protected async override Task<string?> GetDefinitionTextAsync() => await SqlMetadataProvider.GetSqlObjectTextAsync(ItemFullName!);

   protected async override Task<ISqlItem?> GetSqlItemAsync() {
      SP = await SqlMetadataProvider.GetSqlStoredProcedureAsync(DBSchema!, ItemName!);

      if (SP is not null && SP.Any()) {
         try {
            ResultSets = (await new SqlResultSets(_sqlAuth.SqlConnectionString(DBName))
               .GetSPResultSetsAsync($"{DBSchema}.{ItemName}", SP)).ResultSets;
         }
         catch (Exception ex) {
            ResultSetError = ex;
         }
      }

      return SP?.FirstOrDefault();
   }
}
