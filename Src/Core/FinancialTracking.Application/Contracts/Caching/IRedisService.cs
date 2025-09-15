using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Contracts.Caching
{
    public interface IRedisService
    {
        object GetDb(int db = 0);
        object GetServer();

        // Async cache API
        Task<T?> GetAsync<T>(string key, int db = 0);
        Task SetAsync<T>(string key, T value, TimeSpan? expiry = null, int db = 0);
        Task RemoveAsync(string key, int db = 0);
    }
}
