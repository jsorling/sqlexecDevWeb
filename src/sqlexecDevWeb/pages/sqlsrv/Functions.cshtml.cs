using Sorling.SqlConnAuthWeb.authentication;
using Sorling.sqlexecDevWeb.extensions;
using Sorling.sqlexecDevWeb.models.pagemodels;
using Sorling.SqlExecMeta;
using Sorling.SqlExecMeta.objects.functions;

namespace Sorling.sqlexecDevWeb.pages.sqlsrv;

public class FunctionsModel(ISqlConnAuthenticationService sqlAuth) : DBItemPageModel(sqlAuth)
{
   public IEnumerable<SqlFunctionDefCmd.Result>? Function { get; private set; }

   protected override SqlGroupFlags GroupFlags => SqlGroupFlags.Functions;

   protected override async Task<IEnumerable<ISqlItem>?> GetSqlListItemsAsync(string? schema)
      => await SqlMetadataProvider.GetSqlObjectsAsync(GroupFlags, schema);

   protected override async Task<IPrevNxtSqlItem?> GetPrevNxtSqlItemAsync(string schema, string name, string? schemaFolder, SqlGroupFlags? filterGroups)
      => await SqlMetadataProvider.GetSqlObjectPrevNxtAsync($"{schema}.{name}", GroupFlags, schemaFolder
         , filterGroups ?? SqlGroupFlags.Objects);

   protected override async Task<ISqlItem?> GetSqlItemAsync(string schema, string name) {
      Function = await SqlMetadataProvider.GetSqlFunctionAsync(schema, name);
      return Function.FirstOrDefault();
   }

   protected override async Task<string?> GetDefinitionTextAsync(string schema, string name)
      => await SqlMetadataProvider.GetSqlObjectTextAsync($"{schema}.{name}");
}
