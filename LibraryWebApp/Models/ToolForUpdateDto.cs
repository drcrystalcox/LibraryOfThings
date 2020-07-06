
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LibraryWebApp.Models {
    public class ToolForUpdateDto{
        
      
        public string Name {get;set;}
        public string Description {get;set;}
        
       // [BsonElement("RentalCost")]
        public decimal DailyCost {get;set;}
        public decimal ReplacementCost {get;set;}
        public int QuantityAvailable {get;set;}
    }
}