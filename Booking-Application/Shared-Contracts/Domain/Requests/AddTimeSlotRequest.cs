namespace Shared_Contracts.Domain.Requests
{
    public class AddTimeSlotRequest
    {
        public string CourtId { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
