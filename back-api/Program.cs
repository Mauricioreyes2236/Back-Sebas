using back_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var conString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddAuthentication(IISDefaults.AuthenticationScheme);
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials());
});
builder.Services.AddMvc(options =>
{
    options.CacheProfiles.Add("Default30",
        new CacheProfile()
        {
            Duration = 0
        });
    options.EnableEndpointRouting = false;
}).SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddNewtonsoftJson(opt =>
{
    opt.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Arrays;
    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
});
// Add services to the container.
builder.Services.AddDbContext<ApiMauContext>(options => options.UseSqlServer(conString));
builder.Services.Configure<IISOptions>(options =>
{
    options.AutomaticAuthentication = true;
    options.ForwardClientCertificate = true;
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors(options => options.WithOrigins
("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
