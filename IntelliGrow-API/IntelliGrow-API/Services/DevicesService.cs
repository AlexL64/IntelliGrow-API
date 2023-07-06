using IntelliGrow_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Xml.Linq;

namespace IntelliGrow_API.Services
{
    public class DevicesService
    {
        private readonly IMongoCollection<Devices> _devicesCollection;

        public DevicesService(
            IOptions<IntelliGrowDatabaseSettings> IntelliGrowkManagerDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                IntelliGrowkManagerDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                IntelliGrowkManagerDatabaseSettings.Value.DatabaseName);

            _devicesCollection = mongoDatabase.GetCollection<Devices>(
                IntelliGrowkManagerDatabaseSettings.Value.DevicesCollectionName);
        }

        // GET
        public async Task<List<Devices>> GetAsync() =>
            await _devicesCollection.Find(_ => true).ToListAsync();

        // GET BY ID
        public async Task<Devices?> GetAsync(string id) =>
            await _devicesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        // GET BY USER
        public async Task<List<Devices>> GetForUserAsync(string user) =>
            await _devicesCollection.Find(x => x.User == user).ToListAsync();

        // POST
        public async Task CreateAsync(Devices newTask) =>
            await _devicesCollection.InsertOneAsync(newTask);

        // PUT
        public async Task UpdateAsync(string id, Devices updatedTask) =>
            await _devicesCollection.ReplaceOneAsync(x => x.Id == id, updatedTask);

        // DELETE
        public async Task RemoveAsync(string id) =>
            await _devicesCollection.DeleteOneAsync(x => x.Id == id);
    }
}
