using Contracts_;
using CountriesApp.Mappers;
using Entities.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Service.Contracts;
using Service.Utils;
using Shared;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class CountryService : ICountryService
    {
        private const string countryListCacheKey = "countryList";
        private const string thirdPartyUrl = "restcountries.com/v3.1/all?fields=name,capital,borders";
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMemoryCache _cache;
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly SemaphoreSlim semaphore = new(1, 1);

        public CountryService(IRepositoryManager repositoryManager, IMemoryCache cache, ILoggerManager loggerManager)
        {
            _repository = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = loggerManager ?? throw new ArgumentNullException(nameof(loggerManager)); ;
        }

        public async Task<IEnumerable<CountryDto>> GetAllCountriesAsync(bool trackChanges, CancellationToken cancellationToken)
        {
            var countriesToReturn = Enumerable.Empty<CountryDto>();
            var cacheData = _cache.Get(countryListCacheKey);
            //await _cache.RemoveAsync(countryListCacheKey, cancellationToken);
            if (_cache.TryGetValue(countryListCacheKey, out IEnumerable<Country>? countriesFromCache))
            {
                if (countriesFromCache is not null && countriesFromCache.Any())
                {
                    countriesToReturn = countriesFromCache.ToDtos();
                }
            }
            else
            {
                try
                {
                    await semaphore.WaitAsync();
                    var countriesFromDb = await _repository.Country.GetAllCountriesAsync(trackChanges, cancellationToken);
                    if (countriesFromDb is not null && countriesFromDb.Any())
                    {
                        var cacheEntryOptions = new MemoryCacheEntryOptions()
                             .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                             .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                             .SetPriority(CacheItemPriority.Normal);
                
                        _cache.Set(countryListCacheKey, countriesFromDb, cacheEntryOptions);
                        countriesToReturn = countriesFromDb.ToDtos();
                    }
                    else
                    {
                        var countriesFromRequest = await GetCountriesFromThirdPartyApi();
                        if (countriesFromRequest is not null && countriesFromRequest.Any())
                        {
                            var countriesToSaveInDb = new List<Country>();
                            foreach (var country in countriesFromRequest)
                            {
                                var entity = country.ToEntity();
                                _repository.Country.CreateCountry(entity);
                                countriesToSaveInDb.Add(entity);
                            }
                            await _repository.SaveAsync(cancellationToken);
                            var cacheEntryOptions = new MemoryCacheEntryOptions()
                                 .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                                 .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                                 .SetPriority(CacheItemPriority.Normal);                          
                            _cache.Set(countryListCacheKey, countriesToSaveInDb, cacheEntryOptions);
                            countriesToReturn = countriesToSaveInDb.ToDtos();
                        }
                    }
                }
                finally
                {
                    semaphore.Release();
                }
            }
            return countriesToReturn;
        }

        private async Task<List<ResponseCountryObject>?> GetCountriesFromThirdPartyApi()
        {
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var response = await _httpClient.GetAsync("https://restcountries.com/v3.1/all?fields=name,capital,borders");
            var content = await response.Content.ReadAsStringAsync();
            var countries = JsonSerializer.Deserialize<List<ResponseCountryObject>>(content, options);
            return countries;
        }
    }
}
