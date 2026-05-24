using InterView.RemoteServer;
using InterView.RemoteServer.Helper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(cfg => cfg.LowercaseUrls = true);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<SettingsModel>();
builder.Services.AddTransient<RemoteServer>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=GetList}/{id?}")
    .WithStaticAssets();


app.Run();
