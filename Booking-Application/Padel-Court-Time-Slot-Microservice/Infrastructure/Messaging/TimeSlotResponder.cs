using EasyNetQ;
using Padel_Court_Time_Slot_Microservice.Application.Interfaces;
using Padel_Court_Time_Slot_Microservice.Domain.Models;
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
                // For adding a time slot
                _bus.Rpc.RespondAsync<AddTimeSlotRequest, AddTimeSlotResponse>(async request =>
                {
                    Console.WriteLine("Received request to add a new time slot");
                    using var scope = _serviceProvider.CreateScope();
                    var repository = scope.ServiceProvider.GetRequiredService<ITimeSlotRepository>();

                    var newTimeSlot = new TimeSlot
                    {
                        CourtId = request.CourtId,
                        StartTime = request.StartTime,
                        EndTime = request.EndTime,
                        Date = request.Date
                    };

                    await repository.AddTimeSlotAsync(newTimeSlot);

                    return new AddTimeSlotResponse
                    {
                        Success = true,
                        TimeSlotId = newTimeSlot.Id
                    };
                });

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error registering RespondAsync: " + ex);
                return Task.CompletedTask;
            }
        }
    }
}
