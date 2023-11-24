using Microsoft.AspNetCore.Mvc;
using Sorling.SqlConnAuthWeb.authentication;

namespace Sorling.sqlexecDevWeb.pages.sqlsrv.components.topnav;

public class TopNavViewComponent : ViewComponent
{
   public Task<IViewComponentResult> InvokeAsync() => Task.FromResult<IViewComponentResult>(View(this));
}
