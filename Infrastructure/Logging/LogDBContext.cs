using MongoDB.Driver;
using TestCQRS3.Domain;
using TestCQRS3.Infrastructure.Logging.Models;

namespace TestCQRS3.Infrastructure.Logging
{
    public class LogDBContext
    {
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _database;

        public LogDBContext(ITestCQRS3DatabaseSettings TestCQRS3DatabaseSettings)
        {
            _mongoClient = new MongoClient(TestCQRS3DatabaseSettings.LogConnectionString);
            _database = _mongoClient.GetDatabase(TestCQRS3DatabaseSettings.LogDBName);
        }

        internal IMongoCollection<LogModel> Log
        {
            get
            {
                return _database.GetCollection<LogModel>("Log");
            }
        }
    }
}
