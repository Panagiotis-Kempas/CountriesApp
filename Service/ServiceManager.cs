using Contracts_;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICountryService> _countryService;
        private readonly Lazy<IBorderService> _borderService;
        private readonly Lazy<ICapitalService> _capitalService;
        public ServiceManager(IRepositoryManager repositoryManager, IMemoryCache cache, ILoggerManager logger)
        {
            _countryService = new Lazy<ICountryService>(() => new CountryService(repositoryManager, cache, logger));
            _borderService = new Lazy<IBorderService>(() => new BorderService(repositoryManager, logger));
            _capitalService = new Lazy<ICapitalService>(() => new CapitalService(repositoryManager, logger));
        }
        public ICountryService CountryService => _countryService.Value;
        public ICapitalService CapitalService => _capitalService.Value;
        public IBorderService BorderService => _borderService.Value;
    }
}
