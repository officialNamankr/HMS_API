using System.ComponentModel.DataAnnotations;

namespace HMS_API.Models.Dto.PutDtos
{
    public class EditTestDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
