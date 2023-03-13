namespace HMS_API.Models.Dto.GetDtos
{
    public class AppointmentPatientViewDTO
    {
        public Guid AppointmentId { get; set; }
        
        public DateOnly Date_Of_Appointment { get; set; }
        
        public TimeOnly Time_Of_Appointment { get; set; }
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        
    }
}
