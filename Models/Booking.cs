/*
    Developed       : V.G.A.P.Kumara (IT20068578)
    Component Type  : Models
    Function        : Train Reservation
    Usage           : Models of the Function
    */

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ead_rest_project.Models
{
        [BsonIgnoreExtraElements]
        public class Booking
        {   
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public string BookingId { get; set; }= String.Empty;
            [BsonElement("fromStation")]
            public string FromStation { get; set; }= String.Empty;
            [BsonElement("toStation")]
            public string ToStation { get; set; }= String.Empty;
            [BsonElement("bookingDate")]
            public DateTime BookingDate { get; set; }
            [BsonElement("class")]
            public string Class { get; set; }= String.Empty;
            [BsonElement("numTickets")]
            public int NumTickets { get; set; }
            [BsonElement("availability")]
            public bool isAvailablity { get; set; }
            [BsonElement("totalCost")]
            public decimal TotalCost { get; set; }
        }

}
