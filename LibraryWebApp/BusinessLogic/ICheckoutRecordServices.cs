using System.Collections.Generic;
using LibraryWebApp.Models;
namespace LibraryWebApp.BusinessLogic{
    public interface ICheckoutRecordServices{
        public IEnumerable<CheckoutRecord> getAllCheckoutRecords();
    public IEnumerable<CheckoutRecordWithItemDetailsDto> getAllCheckoutRecordsWithItemDetails();

        public CheckoutRecord GetCheckoutRecordById(string checkoutRecordId);
        public CheckoutRecord CreateCheckoutRecord(CheckoutRecordForCreationDto checkoutRecordForCreation);
        public void UpdateCheckoutRecord(string id, CheckoutRecordForCheckInUpdateDto checkoutRecordForUpdate);
		public void DeleteCheckoutRecord(CheckoutRecord checkoutRecordToDelete);
        
    }
}