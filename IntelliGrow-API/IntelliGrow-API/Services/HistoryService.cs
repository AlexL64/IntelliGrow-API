using IntelliGrow_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace IntelliGrow_API.Services
{
    public class HistoryService
    {
        private readonly IMongoCollection<History> _historyCollection;

        public HistoryService(
            IOptions<IntelliGrowDatabaseSettings> IntelliGrowkManagerDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                IntelliGrowkManagerDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                IntelliGrowkManagerDatabaseSettings.Value.DatabaseName);

            _historyCollection = mongoDatabase.GetCollection<History>(
                IntelliGrowkManagerDatabaseSettings.Value.HistoryCollectionName);
        }

        // GET
        public async Task<List<History>> GetAsync() =>
            await _historyCollection.Find(_ => true).ToListAsync();

        // GET BY ID
        public async Task<History?> GetAsync(string id) =>
            await _historyCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        // GET BY DEVICE
        public async Task<List<History>> GetForDeviceAsync(string device) =>
            await _historyCollection.Find(x => x.Device == device).ToListAsync();

        // POST
        public async Task CreateAsync(History newTask) =>
            await _historyCollection.InsertOneAsync(newTask);

        // PUT
        public async Task UpdateAsync(string id, History updatedTask) =>
            await _historyCollection.ReplaceOneAsync(x => x.Id == id, updatedTask);

        // DELETE
        public async Task RemoveAsync(string id) =>
            await _historyCollection.DeleteOneAsync(x => x.Id == id);
    }
}
