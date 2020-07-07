
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace LibraryWebApp.Models {
    public class CheckoutRecordForCheckInUpdateDto{

        public DateTime DateReturned {get;set;}
        public IList<string> Notes {get;set;}
        public Decimal AmountPaid {get;set;}

        
   
    }
}