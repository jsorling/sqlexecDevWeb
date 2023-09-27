using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sorling.SqlConnAuthWeb.authentication;
using System.ComponentModel.DataAnnotations;

namespace Sorling.sqlexecDevWeb.pages;

public class IndexModel : PageModel
{
   protected readonly ISqlConnAuthenticationService _sqlauth;

   public IndexModel(ISqlConnAuthenticationService sqlConnAuthenticationService) => _sqlauth = sqlConnAuthenticationService;

   public SqlConnAuthenticationOptions SqlConnAuthenticationOptions => _sqlauth.Options;

   public class ConnectModel
   {
      [Required(AllowEmptyStrings = false, ErrorMessage = "Required: SQL server address")]
      [Display(Name = "SQL Server address")]
      public string? SqlServer { get; set; }

      [Required(AllowEmptyStrings = false, ErrorMessage = "Required: user name")]
      [Display(Name = "User name")]
      public string? UserName { get; set; }
   }

   [BindProperty]
   public ConnectModel? Input { get; set; }

   public IActionResult OnGet() {
      Input = new();
      return Page();
   }

   public IActionResult OnPost() => ModelState.IsValid
      //? Redirect(Sorling.SQLConnAuthWeb.SQLConnAuthHelper.GetUriEscapedSrvPath(Input!.SqlServer, Input.UserName))
      ? Redirect(_sqlauth.UriEscapedPath(Input?.SqlServer, Input?.UserName))
      : Page();
}
