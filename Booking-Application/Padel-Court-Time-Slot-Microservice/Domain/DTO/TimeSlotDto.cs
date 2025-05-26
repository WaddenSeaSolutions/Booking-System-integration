namespace Padel_Court_Time_Slot_Microservice.Domain.DTO
{
    public class TimeslotDto
    {
        public int CourtId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime Date { get; set; }

    }
}