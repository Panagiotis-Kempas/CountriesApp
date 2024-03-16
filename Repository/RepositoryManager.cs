using Contracts_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<ICountryRepository> _countryRepository;
        private readonly Lazy<IBorderRepository> _borderRepository;
        private readonly Lazy<ICapitalRepository> _capitalRepository;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _countryRepository = new Lazy<ICountryRepository>(() => new CountryRepository(repositoryContext));
            _borderRepository = new Lazy<IBorderRepository>(() => new BorderRepository(repositoryContext));
            _capitalRepository = new Lazy<ICapitalRepository>(() => new CapitalRepository(repositoryContext));
        }
        public ICountryRepository Country => _countryRepository.Value;

        public IBorderRepository Border => _borderRepository.Value;

        public ICapitalRepository Capital => _capitalRepository.Value;

        public async Task SaveAsync(CancellationToken cancellationToken) => await _repositoryContext.SaveChangesAsync(cancellationToken);
    }
}
