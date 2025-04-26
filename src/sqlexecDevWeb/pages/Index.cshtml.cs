using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Sorling.SqlConnAuthWeb.authentication;
using System.ComponentModel.DataAnnotations;

namespace Sorling.sqlexecDevWeb.pages;

public class IndexModel(IOptions<SqlAuthOptions> options, SqlAuthAppPaths sqlAuthAppPaths) : PageModel
{
   public SqlAuthOptions SQLAuthOptions { get; } = options.Value ?? throw new ArgumentNullException(nameof(options));

   public class ConnectModel
   {
      [Required(AllowEmptyStrings = false, ErrorMessage = "Required: SQL server address")]
      [Display(Name = "SQL Server address")]
      public string SqlServer { get; set; } = "";

      [Required(AllowEmptyStrings = false, ErrorMessage = "Required: user name")]
      [Display(Name = "User name")]
      public string UserName { get; set; } = "";
   }

   [BindProperty]
   public ConnectModel Input { get; set; } = new();

   public IActionResult OnGet() {
      Input = new();
      return Page();
   }

   public IActionResult OnPost() => ModelState.IsValid
      ? Redirect(sqlAuthAppPaths.UriEscapedSqlPath(Input.SqlServer, Input.UserName))
      : Page();
}
