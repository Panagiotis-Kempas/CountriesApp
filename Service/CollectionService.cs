using Entities;
using Entities.Exceptions;
using Service.Contracts;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CollectionService : ICollectionService
    {
        public ResponseObj ReturnSecondLargestInteger(RequestObj requestOb)
        {
            int secondLargestInteger;
            if(requestOb.RequestArrayObj.Count() < 1)
            {
                throw new EmptyCollectionException();
            }

            if (requestOb.RequestArrayObj.Count() == 1)
            {
                secondLargestInteger = requestOb.RequestArrayObj.First();
            }
            else
            {
                var descendingCollection = requestOb.RequestArrayObj.OrderByDescending(x => x).ToList();
                secondLargestInteger = descendingCollection[1];
            }
            return new ResponseObj { SecondLargestInteger = secondLargestInteger };
        }
    }
}
