using Microsoft.AspNetCore.Mvc;

namespace Sorling.sqlexecDevWeb.pages.shared.components.topnav;

public class TopNavViewComponent : ViewComponent
{
   public string NowString => DateTime.Now.ToString("s");
   public Task<IViewComponentResult> InvokeAsync() => Task.FromResult<IViewComponentResult>(View(this));
}
