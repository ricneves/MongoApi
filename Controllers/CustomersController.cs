using Microsoft.AspNetCore.Mvc;
using MongoApi.Data;
using MongoApi.Entities;
using MongoDB.Driver;

namespace MongoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IMongoCollection<Customer>? _customers;

        public CustomersController(MongoDbService mongoDbService)
        {
            _customers = mongoDbService.Database?.GetCollection<Customer>("customers");
        }

        [HttpGet]
        public async Task<IEnumerable<Customer>> Get()
        {
            return await _customers.Find(FilterDefinition<Customer>.Empty).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer?>> GetById(string id)
        {
            var filter = Builders<Customer>.Filter.Eq(x => x.Id, id);
            var customer = _customers.Find(filter).FirstOrDefault();

            return customer is not null ? Ok(customer) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Customer customer)
        {
            await _customers.InsertOneAsync(customer);
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

        [HttpPut]
        public async Task<ActionResult> Update(Customer customer)
        {
            var filter = Builders<Customer>.Filter.Eq(x => x.Id, customer.Id);
            await _customers.ReplaceOneAsync(filter, customer);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(string id)
        {
            var filter = Builders<Customer>.Filter.Eq(x => x.Id, id);
            await _customers.DeleteOneAsync(filter);
            return Ok();
        }
    }
}
