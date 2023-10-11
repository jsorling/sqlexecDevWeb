using Sorling.SqlConnAuthWeb;
using Sorling.SqlConnAuthWeb.extenstions;
using Sorling.SqlConnAuthWeb.razor;
using Sorling.sqlexecDevWeb.filters;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlConnAuthentication(o => {
   o.AllowWinauth = false;
   o.ThemeSwitcherLocalStorageName = "theme";
   o.SqlRootPath = "sqlsrv";
})
   .AddSqlConnAuthorization().AddRazorPages(options => {
      _ = options.Conventions.AuthorizeFolder("/sqlsrv", SqlConnAuthConsts.SQLCONNAUTHPOLICY);
      options.Conventions.Add(new SqlConnAuthPageRouteModelConvention("sqlsrv"));
      _ = options.Conventions.AddFolderApplicationModelConvention("/sqlsrv", model
         => model.Filters.Add(new SchemaNObjectFilter("schema", "obj", "filter")));
   });

WebApplication app = builder.Build();
app.UseHttpsRedirection().UseStaticFiles().UseRouting()
   .UseAuthentication().UseAuthorization();

app.MapRazorPages();

app.Run();
