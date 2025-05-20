using EasyNetQ;
using Paddle_Court_Microservice.Infrastructure.Messaging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton(RabbitHutch.CreateBus("host=rabbitmq"));
builder.Services.AddScoped<PaddleCourtClient>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger in all environments (optional)
app.UseSwagger();
app.UseSwaggerUI();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();
app.Run();