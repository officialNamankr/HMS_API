using System.ComponentModel.DataAnnotations.Schema;

namespace HMS_API.Models.Dto.GetDtos
{
    public class ViewMedicalReport
    {
       
        public Guid MedicalReportId { get; set; }

        public Guid AppointmentId { get; set; }
        public DateOnly DateOfExamination { get; set; }
        public TimeOnly TimeOfExamination { get; set; }
        public string Remarks { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; } 
        public virtual RecommendedTest RecommendedTest { get; set; }
    }
}
