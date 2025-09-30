using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Hotel_Mang.Models
{
    public class Purchase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("itemName")]
        public string ItemName { get; set; } = string.Empty;

        [BsonElement("brand")]
        public string Brand { get; set; } = string.Empty;

        [BsonElement("quantity")]
        public decimal Quantity { get; set; }

        [BsonElement("unit")]
        public string Unit { get; set; } = string.Empty; // "Kg", "Litre", "Tin"

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("vendorId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string VendorId { get; set; } = string.Empty;

        [BsonElement("totalAmount")]
        public decimal TotalAmount => Quantity * Price;

        [BsonElement("createdBy")]
        public string CreatedBy { get; set; } = string.Empty;

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
