using Aviator.Main.Database;
using Aviator.Main.Services;
using Aviator.Main.Worker;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddServerSideBlazor().AddInteractiveServerComponents();

builder.Services.AddDbContext<AviatorContext>(s => s.UseSqlite("Data source=./aviator.db"));
builder.Services.AddSingleton<AviatorRepository>();
builder.Services.AddSingleton<AviatorRouterOutputService>();
builder.Services.AddSingleton<AviatorRouterService>();
builder.Services.AddHostedService<AviatorRouterWorker>();

await using var app = builder.Build();

if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseAntiforgery();

app.MapRazorPages();

app.MapBlazorHub();

await app.RunAsync();