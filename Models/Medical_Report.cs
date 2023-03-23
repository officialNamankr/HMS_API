using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS_API.Models
{
    public class Medical_Report
    {
        [Key]
        public Guid MedicalReportId { get; set; }
        public Guid AppointmentId { get; set; }
        [ForeignKey("AppointmentId")]
        public Appointment Appointment { get; set; }
        public string Remarks { get; set; }
        public virtual RecommendedTest RecommendedTest { get; set; } = new RecommendedTest();

        






        
    }
}
