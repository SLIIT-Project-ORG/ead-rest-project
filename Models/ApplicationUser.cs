using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace ead_rest_project.Models
{
	[CollectionName("users")]
	public class ApplicationUser : MongoIdentityUser<Guid>

	{
		public string FullName { get; set; } = String.Empty;

	}
}
