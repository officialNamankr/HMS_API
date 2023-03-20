namespace HMS_API.Models.Dto.PostDtos
{
    public class AddMedicalReport
    {
        public Guid AppointmentId { get; set; }
        public DateTime DateTimeOfExamination { get; set; }
        public string Remarks { get; set; }
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public virtual RecommendedTest RecommendedTest { get; set; }
    }
}
