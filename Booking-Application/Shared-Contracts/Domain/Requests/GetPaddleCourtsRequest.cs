namespace Shared_Contracts.Domain.Requests
{
    public class GetPaddleCourtsRequest
    {
        public string RequestId { get; set; } = Guid.NewGuid().ToString();
    }
}
