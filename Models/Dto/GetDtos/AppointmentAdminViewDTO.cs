namespace HMS_API.Models.Dto.GetDtos
{
    public class AppointmentAdminViewDTO
    {
        public Guid AppointmentId { get; set; }

        public DateOnly Date_Of_Appointment { get; set; }

        public TimeOnly Time_Of_Appointment { get; set; }

        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
    }
}
