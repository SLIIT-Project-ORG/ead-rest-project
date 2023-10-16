/*
 Developed       : Tharuka Gayashan (IT20186906)
 Function        : Train Management
 Component Type  : Service Implementation class
 Filename        : TrainService.cs
 Usage           : For method implementation
*/

using ead_rest_project.Models;
using MongoDB.Driver;
using System.Data.Common;

namespace ead_rest_project.services
{
    public class TrainService : ITrainService
    {
        //Read only train collection instance
        private readonly IMongoCollection<Train> _trains;

        //Get database connection details from AppSettings file and TrainStorageDatabaseSettings.cs file
        public TrainService(ITrainStoreDatabaseSettings settings, IMongoClient mongoClient) { 
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _trains = database.GetCollection<Train>(settings.TrainCollectionName);
        }

        //Create train method Implementation
        public Train CreateTrain(Train train)
        {
            //Checking trainName is Empty or null
            if (train.trainName == null || train.trainName.Equals(""))
            {
                throw new Exception("trainName cannot be null or empty");
            }
            //Checking train capacity is null
            if (train.capacity == null)
            {
                throw new Exception("capacity cannot be null");
            }
            //Checking description is Empty or null;
            if (train.description == null || train.description.Equals(""))
            {
                throw new Exception("description cannot be null or empty");
            }
            //Checking trainTypeId is null
            if(train.trainTypeId == null)
            {
                throw new Exception("trainTypeId cannot be null");
            }

            //Set traing object to trainObj object
            Train trainObj = train;
            //set isActive boolean to true
            trainObj.isActive = true;

            try
            {
                //Save train object to MongoDB collection
                _trains.InsertOne(trainObj);
                Console.WriteLine("New Train Inserted..");

                //return
                return trainObj;
            }catch(DbException e)
            {
                throw new Exception(e.Message);
            }
           
        }

        //Get all train method implementation
        public List<Train> GetAllTrains()
        {
            //Get train list from MongoDB collection
            List<Train> trainList = new List<Train>();
            trainList = _trains.Find(Train => true).ToList();
            
            //Checking train list empty or not
            if(trainList.Count == 0)
            {
                throw new Exception("Trains not found");
            }
            else
            {
                //return train list
                return trainList;
            }
        }

        //Get train by ID method implementation
        public Optional<Train> GetTrainById(string id)
        {
            //Create train Optional object
            Optional<Train> train = null;

            //Checking train exist for given trainId
            train = _trains.Find(Train => Train.trainId == id).FirstOrDefault();
            //Checking selected train available or not
            if(train.HasValue != true)
            {
                throw new Exception("Train not found for given ID");
            }
            else
            {
                //Return selected train
                return train;
            }
        }

        //Update train method implementation
        public void UpdateTrain(string id, Train train)
        {
            //Checking id is null or not
            if(id == null || id.Equals("")) 
            {
                throw new Exception("trainId cannot be null");
            }
            //Checking trainName is null or not
            if (train.trainName == null || train.trainName.Equals(""))
            {
                throw new Exception("trainName cannot be null or empty");
            } 
            //Checking capacity is null
            if (train.capacity == null)
            {
                throw new Exception("capacity cannot be null");
            }
            //Checking description is null or empty
            if (train.description == null || train.description.Equals(""))
            {
                throw new Exception("description cannot be null or empty");
            }

            //Checking trainTypeId is null or not
            if (train.trainTypeId == null)
            {
                throw new Exception("trainTypeId cannot be null");
            }

            Optional<Train> optTrain = null;
            //Checking train exist or not for given trainId
            optTrain = _trains.Find(Train => Train.trainId == id).FirstOrDefault();
            if (optTrain.HasValue != true)
            {
                throw new Exception("Train not found for given ID");
            }

            //Set optTrain details to trainObject
            Train trainDetails = optTrain.Value;

            //Set update request details to trainDetails object
            trainDetails.trainName = train.trainName;
            trainDetails.description = train.description;
            trainDetails.capacity = train.capacity;
            trainDetails.trainTypeId = train.trainTypeId;
            trainDetails.isActive = train.isActive;

            //Saving updated train object to mongo DB
            _trains.FindOneAndReplaceAsync(id, trainDetails);
            Console.WriteLine("Train Details Updated..");
        }

        //Delete train method implementation
        public void DeleteTrain(string id)
        {
            //Checking id is null or empty
            if(id  == null || id.Equals("")) 
            { 
                throw new Exception("trainId cannot be null");
            }
            else
            {
                //Checking train exist or not for given trainId
                Optional<Train> optTrain = _trains.Find(Train => Train.trainId.Equals(id)).FirstOrDefault();
                if (optTrain.HasValue != true)
                {
                    throw new Exception("Train not found for given trainId");
                }
                else
                {
                    //If exist, Delete train by given trainId
                    _trains.DeleteOne(Train => Train.trainId.Equals(id));
                    Console.WriteLine("Train details deleted..");
                }
            }
        }
    }
}
