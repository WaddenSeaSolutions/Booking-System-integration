using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Paddle_Court_Microservice.Domain.Models
{
    public class PaddleCourt
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
