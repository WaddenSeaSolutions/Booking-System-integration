using EasyNetQ;
using MongoDB.Driver;
using Padel_Court_Time_Slot_Microservice.Application.Interfaces;
using Padel_Court_Time_Slot_Microservice.Application.Services;
using Padel_Court_Time_Slot_Microservice.Infrastructure.Messaging;
using Padel_Court_Time_Slot_Microservice.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(DBUtils.ProperlyFormattedConnectionString));
builder.Services.AddSingleton(RabbitHutch.CreateBus("host=rabbitmq;username=rabbit;password=rabbitpw;timeout=60"));
builder.Services.AddScoped<ITimeSlotRepository, TimeSlotRepository>();
builder.Services.AddScoped<ITimeSlotService, TimeSlotService>();
builder.Services.AddHostedService<TimeSlotResponder>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();