namespace Shared_Contracts.Domain.Responses
{
    public class AddTimeSlotResponse
    {
        public bool Success { get; set; }
        public string? TimeSlotId { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
