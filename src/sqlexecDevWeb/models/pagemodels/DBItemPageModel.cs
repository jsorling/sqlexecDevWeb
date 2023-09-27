using Microsoft.AspNetCore.Mvc;
using Sorling.SqlConnAuthWeb.authentication;
using Sorling.SqlExecMeta;

namespace Sorling.sqlexecDevWeb.models.pagemodels;

public abstract class DBItemPageModel : DBSchemaPageModel
{
   protected DBItemPageModel(ISqlConnAuthenticationService sqlAuth) : base(sqlAuth) {
   }

   protected abstract SqlGroupFlags GroupFlags { get; }

   public IEnumerable<ISqlItem>? DBItems { get; private set; }

   protected virtual async Task<IEnumerable<ISqlItem>?> GetSqlListItemsAsync()
      => await Task.FromResult<IEnumerable<ISqlItem>?>(null);

   //@page "{db}/{schema?}/{obj?}/{filter:int?}/{filterschema?}"
   [BindProperty(Name = "obj", SupportsGet = true)]
   public string? ItemName { get; set; }

   [BindProperty(Name = "filter", SupportsGet = true)]
   public int? FilterInt { get; set; }

   [BindProperty(Name = "filterschema", SupportsGet = true)]
   public string? FilterSchema { get; set; }

   public SqlGroupFlags FilterGroupFlags => FilterInt.HasValue ? (SqlGroupFlags)FilterInt.Value : SqlGroupFlags.Objects;

   public string? DefinitionText { get; private set; }

   public Exception? DefinitionTextException { get; private set; }

   public string? ItemFullName => $"{DBSchema}.{ItemName}";

   public IPrevNxtSqlItem? SqlItemPrevNxt { get; private set; }

   protected virtual async Task<string?> GetDefinitionTextAsync() => await Task.FromResult<string?>(null);

   public ISqlItem? DBItem { get; private set; }

   protected abstract Task<ISqlItem?> GetSqlItemAsync();

   protected virtual async Task<IPrevNxtSqlItem?> GetPrevNxtSqlItemAsync()
      => await Task.FromResult<IPrevNxtSqlItem?>(null);

   public async Task<IActionResult> OnGetAsync() {
      if (!string.IsNullOrEmpty(ItemName)) {
         DBItem = await GetSqlItemAsync();
         if (DBItem == null)
            return NotFound();

         try {
            //DefinitionText = await SqlMetadataProvider.GetSqlObjectTextAsync(ItemFullName!);
            DefinitionText = await GetDefinitionTextAsync();
         }
         catch (Exception ex) {

            DefinitionTextException = ex;
         }

         SqlItemPrevNxt = await GetPrevNxtSqlItemAsync();

         return Page();
      }
      else {
         FilterInt = (int)GroupFlags;
         FilterSchema = DBSchema;
         DBItems = await GetSqlListItemsAsync();
         return DBItems is null ? NotFound() : Page();
      }
   }
}
