using MongoDB.Driver;
using Paddle_Court_Microservice.Application.Interfaces;
using Paddle_Court_Microservice.Domain.Services;
using Paddle_Court_Microservice.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(DBUtils.ProperlyFormattedConnectionString));

builder.Services.AddScoped<IMongoDatabase>(sp =>
    sp.GetRequiredService<IMongoClient>().GetDatabase("paddlecourtdb"));
builder.Services.AddScoped<IPaddleCourtService, PaddleCourtService>();
builder.Services.AddScoped<IPaddleCourtRepository, PaddleCourtRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
