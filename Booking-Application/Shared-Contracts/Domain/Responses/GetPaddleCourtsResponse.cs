using Shared_Contracts.Domain.DTOs;

namespace Booking_Microservice.Domain.Responses
{
    public class GetPaddleCourtsResponse
    {
        public string RequestId { get; set; }
        public List<PaddleCourtDto> Courts { get; set; } = new();
    }
}
