using HMS_API.Models.Dto.PostDtos;
using System.ComponentModel.DataAnnotations;

namespace HMS_API.Models.Dto.PutDtos
{
    public class EditDoctorDto
    {

        public string Name { get; set; }
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public ICollection<DepartmentIdDto> Departments { get; set; }
    }
}
