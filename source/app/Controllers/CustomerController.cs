using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Entities;
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

        [HttpGet("{name}")]
        public async Task<ActionResult<CustomerEntity>> GetCustomerAsync(string name)
        {
            var customer = await _repository.GetCustomerAsync(name);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }
            
    }
}
