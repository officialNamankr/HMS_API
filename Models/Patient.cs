using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS_API.Models
{
    public class Patient
    {
        [Key]
        public string PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual ApplicationUser User { get; set; }




    }
}
