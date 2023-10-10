namespace ead_rest_project.Dtos
{
    public class RegisterRequest
	{
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string mobileNo { get; set; }
        public string password { get; set; }
        public string nic { get; set; }
        public string gender { get; set; }
        public int age { get; set; }
        public string imageRef { get; set; }
        public string description { get; set; }
        public int roleId { get; set; }
    }
}
