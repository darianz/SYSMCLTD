using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SYSMCLTDR.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
     
        public DateTime Created { get; set; }
    }
}
