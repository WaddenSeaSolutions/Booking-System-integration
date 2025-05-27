using Booking_Microservice.Application.Interfaces;
using Booking_Microservice.Domain.Services;
using Booking_Microservice.Infrastructure.Messaging;
using Booking_Microservice.Infrastructure.Repositories;
using EasyNetQ;
using Npgsql;
using Paddle_Court_Microservice.Infrastructure.Messaging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton(RabbitHutch.CreateBus("host=rabbitmq;username=rabbit;password=rabbitpw"));
builder.Services.AddScoped<PaddleCourtClient>();
builder.Services.AddScoped<TimeSlotClient>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<NpgsqlConnection>(_ => new NpgsqlConnection(DBUtils.ProperlyFormattedConnectionString));




builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBookingService, BookingService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();
app.Run();