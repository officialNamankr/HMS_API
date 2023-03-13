using System.ComponentModel.DataAnnotations;

namespace HMS_API.Models.Dto.PostDtos
{
    public class AddAppointment
    {
        [Required]
        public DateOnly Date_Of_Appointment { get; set; }
        [Required]
        public TimeOnly Time_Of_Appointment { get; set; }
        [Required]
        public string PatientId { get; set; }
        

        [Required]
        public string DoctorId { get; set; }
        
    }
}
