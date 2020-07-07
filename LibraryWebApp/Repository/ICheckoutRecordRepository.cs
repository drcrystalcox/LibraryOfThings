using System.Collections.Generic;
using LibraryWebApp.Models;
namespace LibraryWebApp.Repository{
    public interface ICheckoutRecordRepository{
         public IEnumerable<CheckoutRecord> GetAllCheckoutRecords() ;
        public CheckoutRecord GetCheckoutRecordById(string checkoutRecordId);

        public CheckoutRecord Create(CheckoutRecord checkoutRecord);
        
        public void Update(string id, CheckoutRecord checkoutRecordIn);

        public void Remove(CheckoutRecord checkoutRecordIn);
        public void Remove(string id);


    }
}