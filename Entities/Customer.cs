using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoApi.Entities
{
    //[BsonIgnoreExtraElements]
    public class Customer
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("customer_name"), BsonRepresentation(BsonType.String)]
        public string? CustomerName { get; set; }

        [BsonElement("email"), BsonRepresentation(BsonType.String)]
        public string? Email { get; set; }

        [BsonElement("active")]
        public bool IsActive { get; set; } = true;
    }
}
