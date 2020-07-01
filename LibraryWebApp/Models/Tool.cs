
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LibraryWebApp.Models {
    public class Tool{
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
       [BsonElement("_id")]
        public string ToolId {get;set;}
        public string Name {get;set;}
        public string Description {get;set;}
        [BsonElement("RentalCost")]

        public decimal DailyCost {get;set;}
        public decimal ReplacementCost {get;set;}
        public int QuantityAvailable {get;set;}
    }
}