using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Hotel_Mang.Models
{
    public class Vendor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("contact")]
        public string Contact { get; set; } = string.Empty;

        [BsonElement("itemsSupplied")]
        public List<string> ItemsSupplied { get; set; } = new List<string>();

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
