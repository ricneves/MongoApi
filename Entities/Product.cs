using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoApi.Entities
{
    public class Product
    {
        [BsonId]
        //[BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
