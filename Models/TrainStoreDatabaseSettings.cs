/*
 Developed       : Tharuka Gayashan (IT20186906)
 Function        : Train Management
 Component Type  : Connection class
 Filename        : TrainStoreDatabaseSettings.cs
 Usage           : To get the details of the database connection
*/


namespace ead_rest_project.Models
{
    public class TrainStoreDatabaseSettings : ITrainStoreDatabaseSettings
    {
        //For database collection name => train
        public string TrainCollectionName { get; set; } = string.Empty;
        //For database connection string => mongodb+srv://
        public string ConnectionString { get; set; } = string.Empty;
        //For database name => ead-db
        public string DatabaseName { get; set; } = string.Empty;
    }
}
