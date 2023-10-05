using ead_rest_project.Models;
using MongoDB.Driver;

namespace ead_rest_project.services
{
    public interface ITrainService
    {
        List<Train> GetAllTrains();
        Optional<Train> GetTrainById(string id);
        Train CreateTrain(Train train);
        void UpdateTrain(string id, Train train);
        void DeleteTrain(string id);
    }
}
