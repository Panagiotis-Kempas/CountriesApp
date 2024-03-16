using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public class CountryDto
    {
        public string CommonName { get; set; }
        public ICollection<CapitalDto>? Capitals { get; set; }
        public ICollection<BorderDto>? Borders { get; set; }
    }
}
