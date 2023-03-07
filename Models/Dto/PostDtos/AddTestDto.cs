using System.ComponentModel.DataAnnotations;

namespace HMS_API.Models.Dto.PostDtos
{
    public class AddTestDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
