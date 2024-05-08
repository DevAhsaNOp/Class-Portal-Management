using Application;
using Application.Features.GenericFeatures;
using Microsoft.AspNetCore.Authentication.Cookies;
using Persistence;
using Persistence.Context;
using WebApp.Extensions;
using WebApp.OptionsSetup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureApplication();
builder.Services.ConfigurePersistence(builder.Configuration);

builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Account/Login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });
builder.Services.ConfigureOptions<ResponseMessagesSetup>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
});
builder.Services.AddRazorPages()
        .AddRazorPagesOptions(options =>
        {
            options.Conventions.AddPageRoute("/Error/Unauthorized", "/Home/Unauthorized");
        });

builder.Configuration.AddJsonFile(builder.Environment.ContentRootPath + @"\wwwroot\ResponseMessages\responseMessages.json", optional: true, reloadOnChange: true);

var app = builder.Build();

var serviceScope = app.Services.CreateScope();
var dataContext = serviceScope.ServiceProvider.GetService<DataContext>();


//App Settings
if (dataContext != null)
{
    AppSetting.TemplatePath = app.Environment.ContentRootPath + @"\wwwroot\Templates\Account";
    AppSetting.DocumentPath = app.Environment.ContentRootPath + @"\wwwroot\Assets\";
    // Get Base URL From Environment Variables Docker Profile
    var baseUrl = Environment.GetEnvironmentVariable("BASE_URL");
    if (!string.IsNullOrEmpty(baseUrl))
        AppSetting.DocumentUrl = baseUrl;
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseErrorHandler();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.UseStatusCodePagesWithRedirects("/Home/Unauthorized");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
