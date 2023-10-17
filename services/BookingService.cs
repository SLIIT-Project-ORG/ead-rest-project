/*
    Developed       : V.G.A.P.Kumara (IT20068578)
    Component Type  : Services
    Function        : Train Reservation
    Usage           : Services of the Functions

    */

using ead_rest_project.Models;
using MongoDB.Driver;
using System.Data.Common;

namespace ead_rest_project.services
{
    public class BookingService : IBookingService
    {
        private readonly IMongoCollection<Booking> _bookings;

        public BookingService(IBookingStoreDatabaseSettings settings, IMongoClient mongoClient) { 
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _bookings = database.GetCollection<Booking>(settings.BookingCollectionName);
        }

        //Create a booking and submit booking details
        public Booking CreateBooking(Booking booking)
        {
            
            if (booking.BookingDate == null || booking.BookingDate.Equals(""))
            {
                throw new Exception("Booking Date cannot be null or empty");
            }
            if(booking.Class == null)
            {
                throw new Exception("Class cannot be null");
            }
            if(booking.NumTickets == null)
            {
                throw new Exception("NumTickets cannot be null");
            }
            if(booking.Availablity == null)
            {
                throw new Exception("isAvailability cannot be null");
            }
            if(booking.TotalCost == null)
            {
                throw new Exception("TotalCost cannot be null");
            }

       
            try
            {
                _bookings.InsertOne(booking);
                Console.WriteLine("New Booking Inserted..");
                return booking;
            }catch(DbException e)
            {
                throw new Exception(e.Message);
            }
           
        }

        //View all the booking records
        public List<Booking> GetAllBookings()
        {
            List<Booking> bookingList = new List<Booking>();
            bookingList = _bookings.Find(Booking => true).ToList();
            
            if(bookingList.Count == 0)
            {
                throw new Exception("Bookings not found");
            }
            else
            {
                return bookingList;
            }
        }

        //View booking according to ID
        public Optional<Booking> GetBookingById(string id)
        {
            Optional<Booking> booking = null;
            booking = _bookings.Find(Booking => Booking.BookingId == id).FirstOrDefault();
            if(booking.HasValue != true)
            {
                throw new Exception("Booking not found for given ID");
            }
            else
            {
                return booking;
            }
        }

        //Delete booking records
        public void DeleteBooking(string id)
        {
            if(id  == null || id.Equals("")) 
            { 
                throw new Exception("bookingId cannot be null");
            }
            else
            {
                Optional<Booking> optBooking = _bookings.Find(Booking => Booking.BookingId.Equals(id)).FirstOrDefault();
                if (optBooking.HasValue != true)
                {
                    throw new Exception("Booking not found for given bookingId");
                }
                else
                {
                    _bookings.DeleteOne(Booking => Booking.BookingId.Equals(id));
                    Optional<Booking> checkBooking = _bookings.Find(Booking => Booking.BookingId.Equals(id)).FirstOrDefault();
                    if (checkBooking.HasValue == true)
                    {
                        throw new Exception("Booking delete request failed");
                    }
                    else
                    {
                        Console.WriteLine("Booking details deleted..");
                    }
                }
            }
        }

     
    }
}
