using Contracts_;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class CountryService : ICountryService
    {
        private readonly IRepositoryManager _repository; 
        private readonly ILoggerManager _logger;
        public CountryService(IRepositoryManager repositoryManager,ILoggerManager loggerManager)
        {
            _repository = repositoryManager;
            _logger = loggerManager;
        }
    }
}
