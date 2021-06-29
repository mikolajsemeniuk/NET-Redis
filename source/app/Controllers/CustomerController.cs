using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Entities;
using app.Inputs;
using app.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace app.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repository;

        public CustomerController(ICustomerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerEntity>> GetCustomersAsync()
        {
            var customer = await _repository.GetCustomersAsync();
            return Ok(customer);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<CustomerEntity>> GetCustomerAsync(string name)
        {
            var customer = await _repository.GetCustomerAsync(name);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost("{name}")]
        public async Task<ActionResult<CustomerEntity>> AddCustomerAsync([FromRoute] string name, [FromBody] CustomerInput input)
        {
            var customer = await _repository.AddCustomerAsync(name, input);
            return Ok(customer);
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult<CustomerEntity>> RemoveCustomerAsync([FromRoute] string name)
        {
            await _repository.RemoveCustomerAsync(name);
            return NoContent();
        }
            
    }
}
