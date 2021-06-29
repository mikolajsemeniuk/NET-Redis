using System.Threading.Tasks;
using app.Entities;

namespace app.Interfaces
{
    public interface ICustomerRepository
    {
        Task<CustomerEntity> GetCustomerAsync(string userName);
    }
}