using ead_rest_project.Models;
using ead_rest_project.services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ead_rest_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainController : ControllerBase
    {

        private readonly ITrainService trainService;

        public TrainController(ITrainService trainService) {
            this.trainService = trainService;
        }

        // GET: api/<TrainController>
        [HttpGet]
        public ActionResult<List<Train>> GetGetAllTrains()
        {
            return trainService.GetAllTrains();
        }

        // GET api/<TrainController>/5
        [HttpGet("{id}")]
        public ActionResult<Optional<Train>> GetGetTrainById(string id)
        {
            return trainService.GetTrainById(id);
        }

        // POST api/<TrainController>
        [HttpPost]
        public ActionResult<Train> CreateTrain([FromBody] Train train)
        {
            return trainService.CreateTrain(train);
        }

        // PUT api/<TrainController>/5
        [HttpPut("{id}")]
        public void UpdateTrain(string id, [FromBody] Train train)
        {
            trainService.UpdateTrain(id,train);
        }

        // DELETE api/<TrainController>/5
        [HttpDelete("{id}")]
        public void DeleteTrain(string id)
        {
            trainService.DeleteTrain(id);
        }
    }
}
