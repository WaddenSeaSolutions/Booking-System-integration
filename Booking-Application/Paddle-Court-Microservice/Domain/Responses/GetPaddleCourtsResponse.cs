using Paddle_Court_Microservice.Domain.DTOs;

namespace Paddle_Court_Microservice.Domain.Responses
{
    public class GetPaddleCourtsResponse
    {
        public string RequestId { get; set; }
        public List<PaddleCourtDto> Courts { get; set; } = new();
    }
}
