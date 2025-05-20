namespace Paddle_Court_Microservice.Domain.Requests
{
    public class GetPaddleCourtsRequest
    {
        public string RequestId { get; set; } = Guid.NewGuid().ToString();
    }
}
