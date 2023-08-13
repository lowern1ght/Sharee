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

builder.Services.AddSingleton(new AuthorizationSession(
        builder.Configuration.GetValue<Guid>("AdminToken")));

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".ss";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAntiforgery(options =>
{
    options.Cookie.Name = ".afz";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = long.MaxValue;
});

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

app.UseSession();

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
