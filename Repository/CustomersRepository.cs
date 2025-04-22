using Microsoft.Extensions.Options;
using MongoApi.Entities;
using MongoApi.Settings;
using MongoDB.Driver;

namespace MongoApi.Repository
{
    public interface ICustomersRepository : IRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetActiveCustomersAsync();
    }

    public class CustomersRepository : Repository<Customer>, ICustomersRepository
    {
        public CustomersRepository(IOptions<MongoDbSettings> settings) : base(settings)
        {
        }

        public async Task<IEnumerable<Customer>> GetActiveCustomersAsync()
        {
            return await _collection.Find(c => c.IsActive != false).ToListAsync();
        }
    }
}
