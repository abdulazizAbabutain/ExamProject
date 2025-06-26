using API.ApiDoc.Tags.Requests;
using API.Filters;
using API.Interfaces;
using API.Middleware;
using API.Services;
using Application;
using Application.Commons.Extensions;
using Infrastructure;
using Infrastructure.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Scalar.AspNetCore;
using Serilog;
using Swashbuckle.AspNetCore.Filters;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    });


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.RegisterInfrastructure();
builder.Services.RegisterApplication();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SchemaFilter<ExampleSchemaFilter>();
    c.ExampleFilters(); 
});



builder.Services.AddSwaggerExamplesFromAssemblyOf<AddTagCommandRequestExample>();


builder.Services
    .AddControllersWithViews()
    .AddXmlSerializerFormatters()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.Converters.Add(new StringEnumConverter()));
// order is vital, this *must* be called *after* addnewtonsoftjson()
builder.Services.AddSwaggerGenNewtonsoftSupport();


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.ControlledBy(LoggingLevelController.LevelSwitch) 
    .WriteTo.Console()
    .WriteTo.Sink(new LoggingService(configuration.GetConnectionString("Examiner")))
    .CreateLogger();


builder.Services.AddScoped<IHttpResultResponder, HttpResultResponder>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevClient", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Host.UseSerilog();
builder.Services.AddSwaggerGen(options =>
{
    // using System.Reflection;
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "API.xml"));
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Application.xml"));

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger(options =>
    {
       options.RouteTemplate = "/openapi/{documentName}.json";
    });
    app.MapScalarApiReference();
}
app.UseMiddleware<ResultMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // During development, proxy to Angular's development server
    //app.UseSpa(spa =>
    //{
    //    spa.Options.SourcePath = "ClientApp";
    //    //spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
    //});
}
//else
//{
//    app.UseSpa(spa =>
//    {
//        spa.Options.SourcePath = "ClientApp/dist";
//        // Add additional configurations for production if needed
//    });
//}
app.UseCors("AllowAngularDevClient");

app.UseAuthorization();

app.MapControllers();

app.Run();
