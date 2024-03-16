using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts_
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetAllCountriesAsync(bool trackChanges, CancellationToken cancellationToken);
        void CreateCountry(Country company);
        void CreateMultipleCountries(IEnumerable<Country> countries);
    }
}
