using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class EmptyCollectionException : BadRequestException
    {
        public EmptyCollectionException() : base("The collection you sent is empty.Please provide a valid collection")
        {
            
        }
    }
}
