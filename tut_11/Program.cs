using tut_11.Services;
using Microsoft.EntityFrameworkCore;
using tut_11;
using tut_11.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Register services
builder.Services.AddScoped<IPrescriptionService, Prescription_Service>();

// Add Swagger for testing in development
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();