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

   protected override async Task<IEnumerable<ISqlItem>?> GetSqlListItemsAsync(string? schema)
      => await SqlMetadataProvider.GetSqlObjectsAsync(GroupFlags, schema);

   public Exception? ResultSetError { get; private set; }

   protected override async Task<IPrevNxtSqlItem?> GetPrevNxtSqlItemAsync(string schema, string name, string? schemaFolder, SqlGroupFlags? filterGroups)
      => await SqlMetadataProvider.GetSqlObjectPrevNxtAsync($"{schema}.{name}", GroupFlags, schemaFolder
         , filterGroups ?? SqlGroupFlags.Objects);

   protected override async Task<string?> GetDefinitionTextAsync(string schema, string name)
      => await SqlMetadataProvider.GetSqlObjectTextAsync($"{schema}.{name}");

   protected override async Task<ISqlItem?> GetSqlItemAsync(string schema, string name) {
      SP = await SqlMetadataProvider.GetSqlStoredProcedureAsync(schema, name);

      if (SP is not null && SP.Any()) {
         try {
            ResultSets = (await new SqlResultSets(_sqlAuth.SqlConnectionString(DBName))
               .GetSPResultSetsAsync($"{schema}.{name}", SP)).ResultSets;
         }
         catch (Exception ex) {
            ResultSetError = ex;
         }
      }

      return SP?.FirstOrDefault();
   }
}
