using Microsoft.Extensions.Options;
using MongoApi.Entities;
using MongoApi.Settings;
using MongoDB.Driver;

namespace MongoApi.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetByPriceRange(decimal minPrice, decimal maxPrice);
    }

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(IOptions<MongoDbSettings> settings) : base(settings)
        {
        }

        public async Task<IEnumerable<Product>> GetByPriceRange(decimal minPrice, decimal maxPrice)
        {
            return await _collection.Find(p => p.Price >= minPrice && p.Price <= maxPrice).ToListAsync();
        }
    }
}
