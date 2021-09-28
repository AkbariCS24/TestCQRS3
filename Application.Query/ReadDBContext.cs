using MongoDB.Driver;
using TestCQRS3.Application.Query.QueryModel;
using TestCQRS3.Domain;

namespace TestCQRS3.Application.Query
{
    public class ReadDBContext
    {
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _database;

        public ReadDBContext(ITestCQRS3DatabaseSettings TestCQRS3DatabaseSettings)
        {
            _mongoClient = new MongoClient(TestCQRS3DatabaseSettings.ConnectionString);
            _database = _mongoClient.GetDatabase(TestCQRS3DatabaseSettings.DatabaseName);
        }

        internal IMongoCollection<ItemQueryModel> Item
        {
            get
            {
                return _database.GetCollection<ItemQueryModel>("Item");
            }
        }
        internal IMongoCollection<Item2QueryModel> Item2
        {
            get
            {
                return _database.GetCollection<Item2QueryModel>("Item2");
            }
        }
    }
}
