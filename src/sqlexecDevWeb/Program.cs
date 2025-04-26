using Sorling.SqlConnAuthWeb.authentication;
using Sorling.SqlConnAuthWeb.extenstions;

SqlAuthAppPaths sqlauthpath = new("/sqlsrv", "srv");
WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlConnAuthentication(sqlauthpath, o => {
   o.AllowIntegratedSecurity = true;
   o.ThemeSwitcherLocalStorageName = "theme";
   o.AllowTrustServerCertificate = true;
   o.AllowLoopbackConnections = true;
   o.AllowPrivateNetworkConnections = true;
})
   .AddSqlConnAuthorization()
   .AddRazorPages()
   .AddSqlAuthRazorPageRouteConventions(sqlauthpath)
   .AuthorizeSqlAuthRootPath(sqlauthpath);

WebApplication app = builder.Build();
app.UseHttpsRedirection()
   .UseStaticFiles()
   .UseRouting()
   .UseAuthentication()
   .UseAuthorization();

app.MapRazorPages();

if (app.Environment.IsDevelopment())
{
   _ = app.MapGet("/debug/routes", (IEnumerable<EndpointDataSource> endpointSources) => {
      string tor = string.Join("\n", endpointSources.SelectMany(source => source.Endpoints));
      return tor;
   });

}

app.Run();
