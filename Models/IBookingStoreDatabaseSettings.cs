/*
    Developed       : V.G.A.P.Kumara (IT20068578)
    Component Type  : Models
    Function        : Train Reservation
    Usage           : Interfaces uses to define databse

    */

namespace ead_rest_project.Models
{
    public interface IBookingStoreDatabaseSettings
    {
        string BookingCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
