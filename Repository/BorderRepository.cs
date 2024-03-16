using Contracts_;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Repository
{
    public class BorderRepository : RepositoryBase<Border>, IBorderRepository
    {
        public BorderRepository(RepositoryContext repositoryContext) : base(repositoryContext) 
        {
        }
    }
}
