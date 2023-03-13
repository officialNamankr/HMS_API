using HMS_API.Controllers;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HMS_API.Models
{
    public class Appointment
    {
        public Guid AppointmentId { get; set; }
        [Required]
        public DateTime Date_Of_Appointment { get; set; }
        [Required]
        public DateTime Time_Of_Appointment { get; set; }
        [Required]
        public string PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set;}

        [Required]
        public string DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }


    }
}
