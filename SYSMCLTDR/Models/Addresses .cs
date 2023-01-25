using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SYSMCLTDR.Models
{
    public class Addresses : BaseEntity
    {
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [ForeignKey("Customers")]
        public int CustomerId { get; set; }
        public Customers Customer { get; set; }
    }
}
