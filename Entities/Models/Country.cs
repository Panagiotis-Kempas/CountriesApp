using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Country
    {
        [Column("CountryId")] 
        public Guid Id { get; set; }
        [Required]
        public string? CommonName { get; set; }
        public ICollection<Capital>? Capitals { get; set; }
        public ICollection<Border>? Borders { get; set; }
    }
}
