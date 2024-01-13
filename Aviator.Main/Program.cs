using Aviator.Main.Database;
using Aviator.Main.Worker;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();
builder.Services.AddDbContext<AviatorContext>(s => s.UseSqlite("Data source=./aviator.db"));
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddHostedService<AviatorRouter>();

await using var app = builder.Build();

if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseAntiforgery();

app.MapRazorPages();

await app.RunAsync();