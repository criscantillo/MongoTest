using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace MongoTest.Models
{
    public class Document
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        [JsonPropertyName("Name")]
        public string DocumentName { get; set; } = null!;

        public int NumPages { get; set; }

        public string Category { get; set; } = null!;

        public string Author { get; set; } = null!;
    }
}
