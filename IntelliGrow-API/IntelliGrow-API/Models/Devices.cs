using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace IntelliGrow_API.Models
{
    public class Devices
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? User { get; set; }

        public int Frequency { get; set; }

        public int Duration { get; set; }
    }
}
