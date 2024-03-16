using Contracts_;
using Microsoft.Extensions.Caching.Distributed;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class CountryService : ICountryService
    {
        private const string countryListCacheKey = "countryList";
        private readonly IRepositoryManager _repository; 
        private readonly ILoggerManager _logger;
        private readonly IDistributedCache _cache;
        private static readonly SemaphoreSlim semaphore = new(1, 1);

        public CountryService(IRepositoryManager repositoryManager, IDistributedCache cache, ILoggerManager loggerManager)
        {
            _repository = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = loggerManager ?? throw new ArgumentNullException(nameof(loggerManager)); ;
        }

        public Task<IEnumerable<CountryDto>> GetAllCountriesAsync(bool trackChanges)
        {
            
            throw new NotImplementedException();
        }
    }
}
