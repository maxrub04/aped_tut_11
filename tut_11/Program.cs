using tut_11.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using tut_11;
using tut_11.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container
builder.Services.AddControllers();

// Register services
builder.Services.AddScoped<IPrescriptionService, Prescription_Service>();

// Add Swagger for testing in development
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();;
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();