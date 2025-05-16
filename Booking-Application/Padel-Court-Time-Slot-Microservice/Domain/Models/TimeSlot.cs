using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Padel_Court_Time_Slot_Microservice.Domain.Models
{
    public class TimeSlot
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("startTime")]
        public DateTime StartTime { get; set; }

        [BsonElement("endTime")]
        public DateTime EndTime { get; set; }

        [BsonElement("isAvailable")]
        public bool IsAvailable { get; set; }
    }
}