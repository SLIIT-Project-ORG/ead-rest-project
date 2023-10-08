namespace ead_rest_project.Models
{
    public class TrainStoreDatabaseSettings : ITrainStoreDatabaseSettings
    {
        public string TrainCollectionName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}
