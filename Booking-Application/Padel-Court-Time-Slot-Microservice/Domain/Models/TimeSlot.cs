using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Padel_Court_Time_Slot_Microservice.Domain.Models
{
    public class TimeSlot
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool IsAvailable { get; set; }
    }
}