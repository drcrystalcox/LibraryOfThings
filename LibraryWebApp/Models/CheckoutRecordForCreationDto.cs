
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace LibraryWebApp.Models {
    public class CheckoutRecordForCreationDto{
        
        
        public string ItemCheckedOutId {get;set;}
        public string CustomerId {get;set;}
        public DateTime DateCheckedOut {get;set;}
        public DateTime DateDue {get;set;}
        public IList<string> Notes {get;set;}
        public Decimal AgreedDailyCost {get;set;}
   
    }
}