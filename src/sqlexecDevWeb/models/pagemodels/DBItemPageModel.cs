using Microsoft.AspNetCore.Mvc;
using Sorling.SqlConnAuthWeb.authentication;
using Sorling.SqlExecMeta;

namespace Sorling.sqlexecDevWeb.models.pagemodels;

public abstract class DBItemPageModel : DBSchemaPageModel
{
   protected DBItemPageModel(ISqlAuthService sqlAuth) : base(sqlAuth) {
   }

   protected abstract SqlGroupFlags GroupFlags { get; }

   public IEnumerable<ISqlItem>? DBItems { get; private set; }

   protected virtual async Task<IEnumerable<ISqlItem>?> GetSqlListItemsAsync(string? schema)
      => await Task.FromResult<IEnumerable<ISqlItem>?>(null);

   //@page "{db}/{schema?}/{obj?}/{filter:int?}/{filterschema?}"
   //[BindProperty(Name = "obj", SupportsGet = true)]
   //public string? ItemName { get; set; }

   [BindProperty(Name = "filter", SupportsGet = true)]
   public int? FilterInt { get; set; }

   [BindProperty(Name = "filterschema", SupportsGet = true)]
   public string? FilterSchema { get; set; }

   public SqlGroupFlags FilterGroupFlags => FilterInt.HasValue ? (SqlGroupFlags)FilterInt.Value : SqlGroupFlags.Objects;

   public string? DefinitionText { get; private set; }

   public Exception? DefinitionTextException { get; private set; }

   public IPrevNxtSqlItem? SqlItemPrevNxt { get; private set; }

   protected virtual async Task<string?> GetDefinitionTextAsync(string schema, string name) => await Task.FromResult<string?>(null);

   public ISqlItem? DBItem { get; private set; }

   protected abstract Task<ISqlItem?> GetSqlItemAsync(string schema, string name);

   protected virtual async Task<IPrevNxtSqlItem?> GetPrevNxtSqlItemAsync(string schema, string name, string? schemaFolder, SqlGroupFlags? filterGroups)
      => await Task.FromResult<IPrevNxtSqlItem?>(null);

   public async Task<IActionResult> OnGetAsync() {
      if (ObejctItemParts is (string, string) parts)
      {
         DBItem = await GetSqlItemAsync(parts.schema, parts.name);
         if (DBItem == null)
            return NotFound();

         try
         {
            //DefinitionText = await SqlMetadataProvider.GetSqlObjectTextAsync(ItemFullName!);
            DefinitionText = await GetDefinitionTextAsync(parts.schema, parts.name);
         }
         catch (Exception ex)
         {

            DefinitionTextException = ex;
         }

         SqlItemPrevNxt = await GetPrevNxtSqlItemAsync(parts.schema, parts.name, SchemaFolder, FilterGroup);

         return Page();
      }
      else
      {
         FilterInt = (int)GroupFlags;
         FilterSchema = SchemaFolder;
         DBItems = await GetSqlListItemsAsync(SchemaFolder);
         return DBItems is null ? NotFound() : Page();
      }
   }
}
