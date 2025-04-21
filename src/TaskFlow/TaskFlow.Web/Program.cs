using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TaskFlow.Web;
using TaskFlow.Web.Data;

#region Configure Bootstrap Logger using serilog 
var configuration = LoggerExtension.ConfigureBootstrapLogger();
#endregion

try
{
    Log.Information("Application starting...");
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

    builder.Services.ServiceRegistration(connectionString);

    #region Serilog integration for Application logs
    builder.Host.ApplicationLogger();
    #endregion

    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<ApplicationDbContext>();

    builder.Services.AddControllersWithViews();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    #region Middleware
    app.UseHttpsRedirection();
    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapStaticAssets();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
        .WithStaticAssets();

    app.MapRazorPages()
       .WithStaticAssets();
    #endregion

    #region Auto Migration
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    try
    {
        await context.Database.MigrateAsync();
        Log.Information("Migrations upto dated ...");
    }
    catch (Exception ex)
    {
        Log.Error(ex, "An error occurd during migrations");
    }

    #endregion

    Log.Information("Application started!");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Fatal Error occurred while starting the application");
}
finally
{
    Log.CloseAndFlush();
}
