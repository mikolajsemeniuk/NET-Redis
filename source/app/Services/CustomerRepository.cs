using System;
using System.Threading.Tasks;
using app.Entities;
using app.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace app.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDistributedCache _redisCache;

        public CustomerRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public async Task<CustomerEntity> GetCustomerAsync(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);

            if (String.IsNullOrEmpty(basket))
                return null;

            return JsonConvert.DeserializeObject<CustomerEntity>(basket);
        }

        public async Task<CustomerEntity> AddCustomerAsync(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);

            if (String.IsNullOrEmpty(basket))
                return null;

            return JsonConvert.DeserializeObject<CustomerEntity>(basket);
        }
    }
}