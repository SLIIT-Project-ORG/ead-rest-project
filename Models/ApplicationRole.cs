using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace ead_rest_project.Models
{
    [CollectionName("roles")]
	public class ApplicationRole : MongoIdentityRole<Guid>
	{
	}
}
