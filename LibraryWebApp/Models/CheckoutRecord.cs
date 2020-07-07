
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace LibraryWebApp.Models {
    public class CheckoutRecord{
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
       [BsonElement("_id")]
        public string CheckoutRecordId {get;set;}

        public string ItemCheckedOutId {get;set;}

        public string CustomerId {get;set;}

        public DateTime DateCheckedOut {get;set;}
        public DateTime DateDue {get;set;}
        public DateTime DateReturned {get;set;}
        public IList<string> Notes {get;set;}
        public decimal AgreedDailyCost {get;set;}
        public decimal AmountPaid {get;set;}

        public Boolean HasBeenReturned {get;set;}

        public override String ToString() =>
        $"Checkout record id: {CheckoutRecordId}\nItem checked out id {ItemCheckedOutId}";
   
    }
}