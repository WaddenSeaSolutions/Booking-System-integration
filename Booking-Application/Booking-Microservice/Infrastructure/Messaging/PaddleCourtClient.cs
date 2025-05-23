
using Booking_Microservice.Domain.Responses;
using EasyNetQ;
using Shared_Contracts.Domain.DTOs;
using Shared_Contracts.Domain.Requests;

namespace Paddle_Court_Microservice.Infrastructure.Messaging
{
    public class PaddleCourtClient
    {
        private readonly IBus _bus;

        public PaddleCourtClient(IBus bus)
        {
            _bus = bus;
        }

        public async Task<List<PaddleCourtDto>> GetPaddleCourtsAsync()
        {
            var request = new GetPaddleCourtsRequest();

            var response = await _bus.Rpc.RequestAsync<GetPaddleCourtsRequest, GetPaddleCourtsResponse>(request);
            

            return response.Courts;
        }
    }
}
