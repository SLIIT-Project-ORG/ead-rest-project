/*
 Developed       : Tharuka Gayashan (IT20186906)
 Function        : Train Management
 Component Type  : Controller
 Filename        : TrainController.cs
 Usage           : For define API endpoints
*/


using ead_rest_project.Models;
using ead_rest_project.services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;


namespace ead_rest_project.Controllers
{
    [Route("api/v1/train")]
    [ApiController]
    public class TrainController : ControllerBase
    {

        //Read only instance from ITrainService
        private readonly ITrainService trainService;

        //Constructor
        public TrainController(ITrainService trainService) {
            this.trainService = trainService;
        }


        //Train get all method (GET Request)
        //Endpoint => {BASE-URL}/api/v1/train
        [HttpGet]
        public ActionResult<List<Train>> GetGetAllTrains()
        {
            return trainService.GetAllTrains();
        }


        //Train get by ID method (GET Request)
        //Endpoint => {BASE-URL}/api/v1/train/{id}
        [HttpGet("{id}")]
        public ActionResult<Optional<Train>> GetGetTrainById(string id)
        {
            return trainService.GetTrainById(id);
        }

        //Train create method (GET Request)
        //Endpoint => {BASE-URL}/api/v1/train
        [HttpPost]
        public ActionResult<Train> CreateTrain([FromBody] Train train)
        {
            return trainService.CreateTrain(train);
        }

        //Update train method (PUT Request)
        //Endpoint => {BASE-URL}/api/v1/train/{id}
        [HttpPut("{id}")]
        public void UpdateTrain(string id, [FromBody] Train train)
        {
            trainService.UpdateTrain(id,train);
        }

        //Train delete method (DELETE Request)
        //Endpoint => {BASE-URL}/api/v1/train/{id}
        [HttpDelete("{id}")]
        public void DeleteTrain(string id)
        {
            trainService.DeleteTrain(id);
        }
    }
}
