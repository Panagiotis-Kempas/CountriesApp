using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Border
    {
        [Column("BorderId")]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }

        [ForeignKey(nameof(Country))]
        public Guid CountryId { get; set; }
        public Country? Country { get; set; }
    }
}
