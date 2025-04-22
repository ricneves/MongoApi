using Microsoft.Extensions.Options;
using MongoApi.Entities;
using MongoApi.Settings;

namespace MongoApi.Repository
{
    public interface ICustomersRepository : IRepository<Customer>
    {        
    }

    public class CustomersRepository : Repository<Customer>, ICustomersRepository
    {
        public CustomersRepository(IOptions<MongoDbSettings> settings) : base(settings)
        {
        }
    }
}
