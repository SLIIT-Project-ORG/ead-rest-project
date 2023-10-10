using System.ComponentModel.DataAnnotations;

namespace ead_rest_project.Dtos
{
    public class RegisterRequest
	{
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string mobileNo { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string nic { get; set; }
        [Required]
        public string gender { get; set; }
        [Required]
        public int age { get; set; }
        [Required]
        public string imageRef { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public int roleId { get; set; }
    }
}
