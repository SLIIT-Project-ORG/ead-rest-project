using ead_rest_project.Models;
using MongoDB.Driver;
using System.Data.Common;

namespace ead_rest_project.services
{
    public class TrainService : ITrainService
    {
        private readonly IMongoCollection<Train> _trains;

        public TrainService(ITrainStoreDatabaseSettings settings, IMongoClient mongoClient) { 
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _trains = database.GetCollection<Train>(settings.TrainCollectionName);
        }

        public Train CreateTrain(Train train)
        {
            if (train.trainName == null || train.trainName.Equals(""))
            {
                throw new Exception("trainName cannot be null or empty");
            }
            if (train.capacity == null)
            {
                throw new Exception("capacity cannot be null");
            }
            if (train.description == null || train.description.Equals(""))
            {
                throw new Exception("description cannot be null or empty");
            }
            if(train.trainTypeId == null)
            {
                throw new Exception("trainTypeId cannot be null");
            }

            Train trainObj = train;
            trainObj.isActive = true;

            try
            {
                _trains.InsertOne(trainObj);
                Console.WriteLine("New Train Inserted..");
                return trainObj;
            }catch(DbException e)
            {
                throw new Exception(e.Message);
            }
           
        }

        public List<Train> GetAllTrains()
        {
            List<Train> trainList = new List<Train>();
            trainList = _trains.Find(Train => true).ToList();
            
            if(trainList.Count == 0)
            {
                throw new Exception("Trains not found");
            }
            else
            {
                return trainList;
            }
        }

        public Optional<Train> GetTrainById(string id)
        {
            Optional<Train> train = null;
            train = _trains.Find(Train => Train.trainId == id).FirstOrDefault();
            if(train.HasValue != true)
            {
                throw new Exception("Train not found for given ID");
            }
            else
            {
                return train;
            }
        }
        public void UpdateTrain(string id, Train train)
        {
            if(id == null || id.Equals("")) 
            {
                throw new Exception("trainId cannot be null");
            }
            if (train.trainName == null || train.trainName.Equals(""))
            {
                throw new Exception("trainName cannot be null or empty");
            }
            if (train.capacity == null)
            {
                throw new Exception("capacity cannot be null");
            }
            if (train.description == null || train.description.Equals(""))
            {
                throw new Exception("description cannot be null or empty");
            }
            if (train.trainTypeId == null)
            {
                throw new Exception("trainTypeId cannot be null");
            }

            Optional<Train> optTrain = null;
            optTrain = _trains.Find(Train => Train.trainId == id).FirstOrDefault();
            if (optTrain.HasValue != true)
            {
                throw new Exception("Train not found for given ID");
            }

            Train trainDetails = optTrain.Value;
            trainDetails.trainName = train.trainName;
            trainDetails.description = train.description;
            trainDetails.capacity = train.capacity;
            trainDetails.trainTypeId = train.trainTypeId;
            trainDetails.isActive = train.isActive;

            _trains.FindOneAndReplaceAsync(id, trainDetails);
            Console.WriteLine("Train Details Updated..");
        }
        public void DeleteTrain(string id)
        {
            if(id  == null || id.Equals("")) 
            { 
                throw new Exception("trainId cannot be null");
            }
            else
            {
                Optional<Train> optTrain = _trains.Find(Train => Train.trainId.Equals(id)).FirstOrDefault();
                if (optTrain.HasValue != true)
                {
                    throw new Exception("Train not found for given trainId");
                }
                else
                {
                    _trains.DeleteOne(Train => Train.trainId.Equals(id));
                    Optional<Train> checkTrain = _trains.Find(Train => Train.trainId.Equals(id)).FirstOrDefault();
                    if (checkTrain.HasValue == true)
                    {
                        throw new Exception("Train delete request failed");
                    }
                    else
                    {
                        Console.WriteLine("Train details deleted..");
                    }
                }
            }
        }
    }
}
