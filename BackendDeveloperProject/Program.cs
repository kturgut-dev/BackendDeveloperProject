using BackendDeveloperProject;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

ServicesStartup startup = new(builder.Configuration, builder.Environment);
startup.ConfigureServices(builder.Services);

WebApplication? app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

startup.Configure(app, app.Environment);

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.MapControllers();

app.Run();
