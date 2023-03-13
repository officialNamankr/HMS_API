using System.ComponentModel.DataAnnotations;

namespace HMS_API.Models.Dto.PostDtos
{
    public class AddAppointmentApi
    {
        public DateTime Time_ofAppointment { get; set; }
        [Required]
        public string PatientId { get; set; }


        [Required]
        public string DoctorId { get; set; }
    }
}
