namespace Booking_Microservice.Domain.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; } = "Pending";

    }
}
