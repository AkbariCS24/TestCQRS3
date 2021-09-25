using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TestCQRS3.Application.Query.QueryModel
{
    public class ItemQueryModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int SqlId { get; set; }
        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string Field3 { get; set; }
    }
}
