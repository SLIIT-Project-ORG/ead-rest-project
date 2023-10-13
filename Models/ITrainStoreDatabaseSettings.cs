/*
 Developed       : Tharuka Gayashan (IT20186906)
 Function        : Train Management
 Component Type  : Connection class interface
 Filename        : ITrainStoreDatabaseSettings.cs
 Usage           : Helper class
*/

namespace ead_rest_project.Models
{
    public interface ITrainStoreDatabaseSettings
    {
        string TrainCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
