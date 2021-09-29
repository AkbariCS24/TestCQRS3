using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TestCQRS3.Infrastructure.Logging.Models
{
    public class LogModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required]
        public string LogType { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
