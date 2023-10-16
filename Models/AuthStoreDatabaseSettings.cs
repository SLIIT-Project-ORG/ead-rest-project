/*
 Developed       : Sachini Rasanga (IT20046552)
 Function        : User Management
 Component Type  : Connection class
 Filename        : AuthStoreDatabaseSettings.cs
 Usage           : To get the details of the database connection
*/

namespace ead_rest_project.Models
{
    public class AuthStoreDatabaseSettings : IAuthStoreDatabaseSettings
    {
        //For database collection name => ex : user
        public string UserCollectionName { get; set; } = string.Empty;
        //For database connection string => ex : mongodb+srv://.....
        public string ConnectionString { get; set; } = string.Empty;
        //For database name => ead-db
        public string DatabaseName { get; set; } = string.Empty;
    }
}
