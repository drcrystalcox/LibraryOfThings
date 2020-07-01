

namespace LibraryWebApp.Models {
    public class Tool{
        public ObjectId ToolId {get;set;}
        public string Name {get;set;}
        public string Description {get;set;}
        public decimal DailyCost {get;set;}
        public decimal ReplacementCost {get;set;}
        public int QuantityAvailable {get;set;}
    }
}