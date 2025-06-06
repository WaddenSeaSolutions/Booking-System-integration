
using EasyNetQ;
using MongoDB.Driver;
using Paddle_Court_Microservice.Application.Interfaces;
using Paddle_Court_Microservice.Domain.Services;
using Paddle_Court_Microservice.Infrastructure.Messaging;
using Paddle_Court_Microservice.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSingleton(RabbitHutch.CreateBus("host=rabbitmq;username=rabbit;password=rabbitpw;timeout=60"));
builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(DBUtils.ProperlyFormattedConnectionString));

builder.Services.AddScoped<IMongoDatabase>(sp =>
    sp.GetRequiredService<IMongoClient>().GetDatabase("paddlecourtdb"));
builder.Services.AddScoped<IPaddleCourtService, PaddleCourtService>();
builder.Services.AddScoped<IPaddleCourtRepository, PaddleCourtRepository>();
builder.Services.AddHostedService<PaddleCourtResponder>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
