using Contracts_;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CapitalRepository : RepositoryBase<Capital>, ICapitalRepository
    {
        public CapitalRepository(RepositoryContext repositoryContext):base(repositoryContext) 
        { 
        }
    }
}
