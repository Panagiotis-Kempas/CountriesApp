using Contracts_;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Repository
{
    public class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {
        public CountryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Country>> GetAllCountriesAsync(bool trackChanges, CancellationToken cancellationToken) =>
            await FindAll(trackChanges)
            .Include(x => x.Borders)
            .Include(x => x.Capitals)
            .OrderBy(c => c.CommonName)
            .ToListAsync(cancellationToken);
        public void CreateCountry(Country company) => Create(company);

        public void CreateMultipleCountries(IEnumerable<Country> countries) => CreateInBulk(countries);
    }
}
