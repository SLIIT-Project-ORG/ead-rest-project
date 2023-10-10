using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace ead_rest_project.Models
{
    [CollectionName("users")]
	public class ApplicationUser
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string userId { get; set; }
		[BsonElement("FIRST_NAME")]
		public string firstName { get; set; }
        [BsonElement("LAST_NAME")]
        public string lastName { get; set; }
        [BsonElement("USERNAME")]
        public string username { get; set; }
        [BsonElement("EMAIL")]
        public string email { get; set; }
        [BsonElement("MOBILE_NO")]
        public string mobileNo { get; set; }
        [BsonElement("PASSWORD")]
        public string password { get; set; }
        [BsonElement("NIC")]
        public string nic { get; set; }
        [BsonElement("GENDER")]
        public string gender { get; set; }
        [BsonElement("AGE")]
        public int age { get; set; }
        [BsonElement("IMAGE_REF")]
        public string imageRef { get; set; }
        [BsonElement("DESCRIPTION")]
        public string description { get; set; }
        [BsonElement("IS_ACTIVE")]
        public bool isActive { get; set; }
        [BsonElement("ROLE_ID")]
        public int roleId { get; set; }
	}
}
