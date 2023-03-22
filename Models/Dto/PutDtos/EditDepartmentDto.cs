using System.ComponentModel.DataAnnotations;

namespace HMS_API.Models.Dto.PutDtos
{
    public class EditDepartmentDto
    {
        [Required]
        public string Name { get; set; }
    }
}
