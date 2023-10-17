/*
    Developed       : V.G.A.P.Kumara (IT20068578)
    Component Type  : Controller
    Function        : Train Reservation
    Usage           : For Control APIs

    */

using ead_rest_project.Models;
using ead_rest_project.services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace ead_rest_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {

        private readonly IBookingService bookingService;

        public BookingController(IBookingService bookingService) {
            this.bookingService = bookingService;
        }

        // GET: api/<BookingController>
        //Endpoint => {BASE-URL}/api/booking
        [HttpGet]
        public ActionResult<List<Booking>> GetGetAllBookings()
        {
            return bookingService.GetAllBookings();
        }

        // GET api/<BookingController>/
        //Endpoint => {BASE-URL}/api/booking
        [HttpGet("{id}")]
        public ActionResult<Optional<Booking>> GetGetBookingById(string id)
        {
            return bookingService.GetBookingById(id);
        }

        // POST api/<BookingController>
        //Endpoint => {BASE-URL}/api/booking
        [HttpPost]
        public ActionResult<Booking> CreateBooking([FromBody] Booking booking)
        {
            return bookingService.CreateBooking(booking);
        }


        // DELETE api/<BookingController>/
        //Endpoint => {BASE-URL}/api/booking
        [HttpDelete("{id}")]
        public void DeleteBooking(string id)
        {
            bookingService.DeleteBooking(id);
        }
    }
}
