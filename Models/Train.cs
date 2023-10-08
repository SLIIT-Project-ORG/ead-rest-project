using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace ead_rest_project.Models
{

    [BsonIgnoreExtraElements]
    public class Train
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string trainId { get; set; } = String.Empty;
        [BsonElement("TRAIN_ID")]
        public string trainName { get; set; } = String.Empty;
        [BsonElement("CAPACITY")]
        public int? capacity { get; set; }
        [BsonElement("DESCRIPTION")]
        public string description { get; set; } = String.Empty;
        [BsonElement("IS_ACTIVE")]
        public bool isActive { get; set; }
        [BsonElement("TRAIN_TYPE_ID")]
        public int? trainTypeId { get; set; }
    }
}
