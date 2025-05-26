using EasyNetQ;
using Padel_Court_Time_Slot_Microservice.Application.Interfaces;
using Shared_Contracts.Domain.DTOs;
using Shared_Contracts.Domain.Requests;
using Shared_Contracts.Domain.Responses;

namespace Padel_Court_Time_Slot_Microservice.Infrastructure.Messaging
{
    public class TimeSlotResponder : BackgroundService
    {
        private readonly IBus _bus;
        private readonly IServiceProvider _serviceProvider;
        public TimeSlotResponder(IBus bus, IServiceProvider serviceProvider) 
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _bus.Rpc.RespondAsync<GetTimeSlotRequest, GetTimeSlotResponse>(async request =>
                {
                    Console.WriteLine("Received request for time slots");
                    using var scope = _serviceProvider.CreateScope();
                    var repository = scope.ServiceProvider.GetRequiredService<ITimeSlotRepository>();

                    var timeSlots = await repository.GetBookedTimeSlotsAsync();

                    return new GetTimeSlotResponse
                    {
                        RequestId = request.RequestId,
                        Times = timeSlots.Select(ts => new TimeSlotData
                        {
                            Id = ts.Id,
                            StartTime = ts.StartTime,
                            EndTime = ts.EndTime,
                            IsAvailable = ts.IsAvailable
                        }).ToList()
                    };
                });
                return Task.CompletedTask;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error registering RespondAsync: " + ex);
                return Task.CompletedTask;
            }
        }
    }
}
