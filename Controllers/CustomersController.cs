using Microsoft.AspNetCore.Mvc;
using MongoApi.Entities;
using MongoApi.Repository;

namespace MongoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersRepository _customersRepository;

        public CustomersController(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customers = await _customersRepository.GetAllAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var customer = await _customersRepository.GetByIdAsync(id);
            return customer is not null ? Ok(customer) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Customer customer)
        {
            await _customersRepository.CreateAsync(customer);
            return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
        }

        [HttpPut]
        public async Task<IActionResult> Update(string id, Customer customer)
        {
            await _customersRepository.UpdateAsync(id, customer);
            return Ok(customer);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _customersRepository.DeleteAsync(id);
            return Ok(new { Sucess = true, Message = "Customer deleted" });
        }
    }
}
