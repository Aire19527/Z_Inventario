using Infraestructure.Core.Data;
using Microsoft.EntityFrameworkCore;
using Web.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


#region SQL SERVER Conecction
builder.Services.AddDbContext<DataContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStringSQLServer"));
});
#endregion

#region Dependency Injection
DependencyInyectionHandler.DependencyInyectionConfig(builder.Services);
#endregion

//#if DEBUG
//builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
//#endif
// Load configuration
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var app = builder.Build();

#region RunSeeding
var scopeFactory = app.Services.GetService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var seeder = scope.ServiceProvider.GetService<SeedDb>();
    seeder!.ExecSeedAsync().Wait();
}
#endregion

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//var configurationBuilder = new ConfigurationBuilder();

//configurationBuilder.AddJsonFile("appsettings.json");
//#if DEBUG
//configurationBuilder.AddJsonFile("appsettings.debug.json");
//#elif RELEASE
//configurationBuilder.AddJsonFile("appsettings.azure.json");
//#endif

app.Run();
