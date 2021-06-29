using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using app.Entities;
using app.Inputs;
using app.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace app.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDistributedCache _redis;
        private readonly IConfiguration _configuration;

        public CustomerRepository(IDistributedCache redis, IConfiguration configuration)
        {
            _redis = redis;
            _configuration = configuration;
        }

        public async Task<IEnumerable<CustomerEntity>> GetCustomersAsync()
        {
            ConfigurationOptions options = ConfigurationOptions.Parse(_configuration.GetConnectionString("DefaultConnection"));
            ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(options);
            IDatabase db = connection.GetDatabase();
            EndPoint endPoint = connection.GetEndPoints().First();
            // "application:DB0:customer:hej:*"
            RedisKey[] keys = connection.GetServer(endPoint).Keys(pattern: "*").ToArray();
            var server = connection.GetServer(endPoint);

            var results = new List<CustomerEntity>();
            foreach (var key in server.Keys())
            {
                var result = await _redis.GetStringAsync(key);
                results.Add(JsonConvert.DeserializeObject<CustomerEntity>(result));
            }
            return results;
        }

        public async Task<CustomerEntity> GetCustomerAsync(string name)
        {
            var customer = await _redis.GetStringAsync(name);

            if (String.IsNullOrEmpty(customer))
                return null;

            return JsonConvert.DeserializeObject<CustomerEntity>(customer);
        }

        public async Task<CustomerEntity> AddCustomerAsync(string name, CustomerInput input)
        {
            await _redis.SetStringAsync(name, JsonConvert.SerializeObject(input));

            return await GetCustomerAsync(name);
        }

        public async Task RemoveCustomerAsync(string name)
        {
            await _redis.RemoveAsync(name);
        }
    }
}