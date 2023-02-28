using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS_API.Models
{
    public class Doctor
    {
        public Doctor()
        {
            this.Departments = new HashSet<Department>();
        }
        [Key]
        public string DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
    }
}
