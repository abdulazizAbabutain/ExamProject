using Scalar.AspNetCore;
using Infrastructure;
using Application;
using Swashbuckle.AspNetCore.Annotations;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.RegisterInfrastructure();
builder.Services.RegisterApplication();
builder.Services.AddSwaggerGen();



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevClient", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // During development, proxy to Angular's development server
    app.UseSpa(spa =>
    {
        spa.Options.SourcePath = "ClientApp";
        //spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
    });
}
else
{
    app.UseSpa(spa =>
    {
        spa.Options.SourcePath = "ClientApp/dist";
        // Add additional configurations for production if needed
    });
}
app.UseCors("AllowAngularDevClient");

app.UseAuthorization();

app.MapControllers();

app.Run();
