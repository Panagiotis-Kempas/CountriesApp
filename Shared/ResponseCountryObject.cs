using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ResponseCountryObject
    {
        public CountryCommonName Name { get; set; }
        public List<string> Capital { get; set; }
        public List<string> Borders { get; set; }
    }

    public class CountryCommonName
    {
        public string Common { get; set; }
    }
}
