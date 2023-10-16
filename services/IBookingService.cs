/*
    Developed       : V.G.A.P.Kumara (IT20068578)
    Component Type  : Services
    Function        : Train Reservation
    Usage           : Block of the definining services
    */

using ead_rest_project.Models;
using MongoDB.Driver;

namespace ead_rest_project.services
{
    public interface IBookingService
    {
        List<Booking> GetAllBookings();
        Optional<Booking> GetBookingById(string id);
        Booking CreateBooking(Booking booking);
        //void UpdateBooking(string id, Booking booking);
        void DeleteBooking(string id);
    }
}
