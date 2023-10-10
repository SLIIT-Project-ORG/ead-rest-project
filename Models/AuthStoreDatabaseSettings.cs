namespace ead_rest_project.Models
{
    public class AuthStoreDatabaseSettings : IAuthStoreDatabaseSettings
    {
        public string UserCollectionName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}
