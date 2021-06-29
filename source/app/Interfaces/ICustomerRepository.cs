using System.Collections.Generic;
using System.Threading.Tasks;
using app.Entities;
using app.Inputs;

namespace app.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerEntity>> GetCustomersAsync();
        Task<CustomerEntity> GetCustomerAsync(string name);
        Task<CustomerEntity> AddCustomerAsync(string name, CustomerInput input);
        Task RemoveCustomerAsync(string name);
    }
}