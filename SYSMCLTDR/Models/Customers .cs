using System.ComponentModel.DataAnnotations;

namespace SYSMCLTDR.Models
{
    public class Customers : BaseEntity
    {

        public Customers()
        {
            Created = DateTime.Now;
        }
       
        public string Name { get; set; }
        [Required]
        [RegularExpression("^[0-9]{9}$", ErrorMessage = "CustomerNumber must be 9 digits long and can only contain numbers.")]
        [StringLength(9, ErrorMessage = "CustomerNumber must be 9 characters long.", MinimumLength = 9)]
        public string CustomerNumber { get; set; }
    }
}
