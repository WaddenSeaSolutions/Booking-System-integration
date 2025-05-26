using Shared_Contracts.Domain.DTOs;

namespace Shared_Contracts.Domain.Responses
{
    public class GetPaddleCourtsResponse
    {
        public string RequestId { get; set; }
        public List<PaddleCourtDto> Courts { get; set; }
    }
}
