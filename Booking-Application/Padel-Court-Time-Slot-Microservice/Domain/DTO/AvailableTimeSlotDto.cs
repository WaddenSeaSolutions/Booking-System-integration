namespace Padel_Court_Time_Slot_Microservice.Domain.DTO
{
    public class AvailableTimeSlotDto
    {
        public string Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsAvailable { get; set; }
    }
}