using EasyNetQ;
using Shared_Contracts.Domain.DTOs;
using Shared_Contracts.Domain.Requests;
using Shared_Contracts.Domain.Responses;

namespace Booking_Microservice.Infrastructure.Messaging
{
    public class TimeSlotClient
    {
        private readonly IBus _bus;
        public TimeSlotClient(IBus bus)
        {
            _bus = bus;
        }
        public async Task<AddTimeSlotResponse> CreateTimeSlotAsync(int courtId, DateTime date, DateTime startTime, DateTime endTime)
        {
            var request = new AddTimeSlotRequest
            {
                CourtId = courtId,
                Date = date,
                StartTime = startTime,
                EndTime = endTime
            };

            var response = await _bus.Rpc.RequestAsync<AddTimeSlotRequest, AddTimeSlotResponse>(request);
            return response;
        }
    }
}
