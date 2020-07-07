using LibraryWebApp.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
 
namespace LibraryWebApp.Repository
{
    public class RepositoryContext
    {

        private readonly IMongoDatabase _db;

        public IMongoCollection<Tool> Tools =>_db.GetCollection<Tool>("Tools");
        public IMongoCollection<CheckoutRecord> CheckoutRecords => _db.GetCollection<CheckoutRecord>("CheckoutRecords");

        public RepositoryContext(IMongoClient client, string database)
        {
            _db = client.GetDatabase(database);
        }
 
     }
}