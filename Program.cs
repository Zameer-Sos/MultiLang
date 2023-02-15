using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using MultiLang;
using MultiLang.Models; 
using System.Globalization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//add services
builder.Services.AddLocalization(o => o.ResourcesPath = "Resources");
builder.Services.AddSingleton<LangService>();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization(o =>
    {
        o.DataAnnotationLocalizerProvider = (type, factory) =>
        {
           // var assemblyName = new AssemblyName(typeof(SResources).GetTypeInfo().Assembly.FullName);
            return factory.Create(typeof(SharedResource));
        };
    });

builder.Services.Configure<RequestLocalizationOptions>(o => 
{
    var supportedCultures = new[]
    {
        new CultureInfo("ar-AE"),
        new CultureInfo("en-US"),
        new CultureInfo("ur")
    };
    o.DefaultRequestCulture = new RequestCulture(culture: "ar-AE",uiCulture: "ar-AE");
    o.SupportedCultures = supportedCultures;
    o.SupportedUICultures = supportedCultures;
    
    o.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
});

//builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
var app = builder.Build();

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

//var supportedCultures = new[] { "en-US", "ar-AE", "ur" };
//var localizationOps = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
  //  .AddSupportedCultures(supportedCultures)
   // .AddSupportedUICultures(supportedCultures);
var locOps = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(locOps.Value);
//app.UseRequestLocalization(o => {
//    o.SetDefaultCulture("en-US");
//    o.AddSupportedCultures("en-US", "ar-AE", "ur");
//    o.AddSupportedUICultures("en-US", "ar-AE", "ur");

//    o.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
//});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
