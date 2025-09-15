using FinancialTracking.Application.Contracts.Caching;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinancialTracking.Caching
{
    public class RedisService : IRedisService, IDisposable
    {
        private readonly Lazy<ConnectionMultiplexer> _lazyConnection;

        public RedisService(IConfiguration configuration)
        {
            var redisHost = configuration["Redis:Host"];
            var redisPort = configuration["Redis:Port"];
            var configString = $"{redisHost}:{redisPort}";

            _lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
                ConnectionMultiplexer.Connect(configString));
        }

        private ConnectionMultiplexer Connection => _lazyConnection.Value;

        // Interface implementasyonu (Application bağımsız)
        public object GetDb(int db = 0) => Connection.GetDatabase(db);
        public object GetServer() => Connection.GetServer(Connection.GetEndPoints().First());

        // -------------------- Redis Async Cache Metodları --------------------
        public async Task<T?> GetAsync<T>(string key, int db = 0)
        {
            var value = await Connection.GetDatabase(db).StringGetAsync(key);
            if (value.IsNullOrEmpty) return default;
            return JsonSerializer.Deserialize<T>(value!);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null, int db = 0)
        {
            var serialized = JsonSerializer.Serialize(value);
            await Connection.GetDatabase(db).StringSetAsync(key, serialized, expiry);
        }

        public async Task RemoveAsync(string key, int db = 0)
        {
            await Connection.GetDatabase(db).KeyDeleteAsync(key);
        }

        // Dispose ConnectionMultiplexer
        public void Dispose()
        {
            if (_lazyConnection.IsValueCreated)
                Connection.Dispose();
        }
    }
}
