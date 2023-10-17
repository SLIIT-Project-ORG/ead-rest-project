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
            [BsonElement("bookingNo")]
            public string BookingNo { get; set; } = String.Empty;
            [BsonElement("fromStation")]
            public string FromStation { get; set; }= String.Empty;
            [BsonElement("toStation")]
            public string ToStation { get; set; }= String.Empty;
            [BsonElement("trainDate")]
            public DateTime TrainDate { get; set; }
            [BsonElement("trainTime")]
            public DateTime TrainTime { get; set; }
            [BsonElement("bookingDate")]
            public DateTime BookingDate { get; set; }
            [BsonElement("class")]
            public int Class { get; set; }
            [BsonElement("numTickets")]
            public int NumTickets { get; set; }
            [BsonElement("availability")]
            public string Availablity { get; set; }
            [BsonElement("totalCost")]
            public int TotalCost { get; set; }
        }

}
