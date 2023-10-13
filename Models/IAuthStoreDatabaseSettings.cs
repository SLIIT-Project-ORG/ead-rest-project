/*
 Developed       : Sachini Rasanga (IT20046552)
 Function        : User Management
 Component Type  : Connection class interface
 Filename        : IAuthStoreDatabaseSettings.cs
 Usage           : Helper class
*/

namespace ead_rest_project.Models
{
    public interface IAuthStoreDatabaseSettings
    {
        string UserCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
