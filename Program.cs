using funda_assignment.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAgentsService, AgentsService>();

builder.Services.AddSingleton<PeriodicHostedService>();
builder.Services.AddHostedService(
    provider => provider.GetRequiredService<PeriodicHostedService>()
);
builder.Services.AddSingleton<funda_assignment.MyMemoryCache>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Agents}/{action=Index}/{id?}");

app.MapGet("/background", (PeriodicHostedService service) =>
{
    return new PeriodicHostedServiceState();
});

app.Run();

