using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace IntelliGrow_API.Models
{
    public class History
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Device { get; set; } = null!;

        public DateTime Date { get; set; }

        public int Duration { get; set; }
    }
}
