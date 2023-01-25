using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SYSMCLTDR.Models
{
    public class Contacts : BaseEntity
    {
        [Required]
        public string FullName { get; set; }
        public string OfficeNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [ForeignKey("Customers")]
        public int CustomerId { get; set; }
        public Customers Customer { get; set; }
    }
}
