/* using Library.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
 
namespace Library.Repository
{
    public class RepositoryContext
    {

        private readonly IMongoDatabase _db;

        public IMongoCollection<ToolItem> Tools =>_db.GetCollection<ToolItem>("Tools");

        public RepositoryContext(IMongoClient client, string database)
        {
            _db = client.GetDatabase(database);
        }
 
     }
} */