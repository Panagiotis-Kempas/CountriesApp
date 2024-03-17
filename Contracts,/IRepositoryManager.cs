using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts_
{
    public interface IRepositoryManager
    {
        ICountryRepository Country { get; }
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
