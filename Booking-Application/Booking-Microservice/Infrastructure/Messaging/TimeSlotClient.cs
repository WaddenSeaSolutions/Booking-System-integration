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
        public async Task<List<TimeSlotData>> GetTimeSlotsAsync()
        {
            var request = new GetTimeSlotRequest();
            var response = await _bus.Rpc.RequestAsync<GetTimeSlotRequest, GetTimeSlotResponse>(request);
            return response.Times;
        }
    }
}
