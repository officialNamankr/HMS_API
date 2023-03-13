<<<<<<< HEAD
﻿using HMS_API.Controllers;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
=======
﻿using System.ComponentModel.DataAnnotations;
>>>>>>> 5daa595f913dc03b44276c08edc1c126652d31e5

namespace HMS_API.Models.Dto.PostDtos
{
    public class AddAppointment
    {
        [Required]
<<<<<<< HEAD
        
        public DateOnly Date_Of_Appointment { get; set; }
        [Required]
        
=======
        public DateOnly Date_Of_Appointment { get; set; }
        [Required]
>>>>>>> 5daa595f913dc03b44276c08edc1c126652d31e5
        public TimeOnly Time_Of_Appointment { get; set; }
        [Required]
        public string PatientId { get; set; }
        

        [Required]
        public string DoctorId { get; set; }
        
    }
}
