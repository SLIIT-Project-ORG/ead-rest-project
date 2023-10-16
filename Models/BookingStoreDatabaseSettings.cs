/*
    Developed       : V.G.A.P.Kumara (IT20068578)
    Component Type  : Models
    Function        : Train Reservation
    Usage           : Fetch Data of the Database

    */

namespace ead_rest_project.Models
{
    public class BookingStoreDatabaseSettings : IBookingStoreDatabaseSettings
    {
        public string BookingCollectionName { get; set; }= string.Empty;
        public string ConnectionString { get; set; }= string.Empty;
        public string DatabaseName { get; set; }= string.Empty;
    }
}