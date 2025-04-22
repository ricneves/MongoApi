using Microsoft.Extensions.Options;
using MongoApi.Settings;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoApi.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly IMongoCollection<T> _collection;

        public Repository(IOptions<MongoDbSettings> settings)
        {
            // Instance of mongo client
            var client = new MongoClient(settings.Value.ConnectionString);

            // Database instance
            var database = client.GetDatabase(settings.Value.DatabaseName);

            // Collection from the database
            //_collection = database.GetCollection<T>(typeof(T).Name);
            var collectionName = typeof(T).Name.ToLower() + "s";
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task CreateAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            var objectId = new ObjectId(id);
            await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", objectId));
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            var objectId = new ObjectId(id);
            return await _collection.Find(Builders<T>.Filter.Eq("_id", objectId)).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(string id, T entity)
        {
            var objectId = new ObjectId(id);
            await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", objectId), entity);
        }
    }
}
