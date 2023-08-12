using Serilog;
using Sharee.Application.Services;
using Sharee.Application.Interfaces;
using Sharee.Application.Data;
using Sharee.Application.Data.Entities;
using Sharee.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

builder.Logging.ClearProviders()
    .AddSerilog(logger);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen();
}

builder.Services.AddRouting(options => 
    options.LowercaseUrls = true);

builder.Services.AddRazorPages();

builder.Services.AddControllers();

builder.Services.AddDbContext<ShareeDbContext>();

var option = new SharingServiceOption(
    "0", 
    "1", 
    builder.Configuration.GetValue<String>(nameof(SharingServiceOption.PathToSharingFolder)) 
        ?? throw new InvalidOperationException($"Not found param {nameof(SharingServiceOption.PathToSharingFolder)}"));

builder.Services.AddSharingOption(option);

builder.Services.AddScoped<ISharingService<Unit>, SharingService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapDefaultControllerRoute();

app.Run();
