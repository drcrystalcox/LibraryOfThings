using System.Collections.Generic;
using LibraryWebApp.Models;
using MongoDB.Driver;
using System;

namespace LibraryWebApp.Repository{
    public class CheckoutRecordRepository : ICheckoutRecordRepository {
        protected RepositoryContext _repositoryContext;

        public CheckoutRecordRepository(RepositoryContext repoContext){
            _repositoryContext = repoContext;
        }

        public IEnumerable<CheckoutRecord> GetAllCheckoutRecords() {
          //  IQueryable<CheckoutRecordItem>  checkoutRecords = _repositoryContext.Set<CheckoutRecordItem>().AsNoTracking();
           // return checkoutRecords.OrderBy(to=>to.Name).ToList();

           var checkoutRecords =_repositoryContext.CheckoutRecords.Find(checkoutRecord=>true );
           return checkoutRecords.ToEnumerable();

           //Find(book => true).ToList()
        }

        public CheckoutRecord GetCheckoutRecordById(string checkoutRecordId) {
            var checkoutRecord =_repositoryContext.CheckoutRecords.Find<CheckoutRecord>(record=>record.CheckoutRecordId==checkoutRecordId).FirstOrDefault();
            return checkoutRecord;
        }

        public CheckoutRecord Create(CheckoutRecord checkoutRecord) {
            _repositoryContext.CheckoutRecords.InsertOne(checkoutRecord);
            Console.WriteLine("checkoutRecord after inserting:"+checkoutRecord.CheckoutRecordId);
            //
            return checkoutRecord;
        }
        
        public void Update(string id, CheckoutRecord checkoutRecordIn){
            _repositoryContext.CheckoutRecords.ReplaceOne(checkoutRecord=>checkoutRecord.CheckoutRecordId==id, checkoutRecordIn);
        }

        public void Remove(CheckoutRecord checkoutRecordIn) {
            _repositoryContext.CheckoutRecords.DeleteOne(checkoutRecord=>checkoutRecord.CheckoutRecordId==checkoutRecordIn.CheckoutRecordId);
        }
        public void Remove(string id) {
            _repositoryContext.CheckoutRecords.DeleteOne(checkoutRecord=>checkoutRecord.CheckoutRecordId==id);
        }
    }
}