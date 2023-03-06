using System.ComponentModel.DataAnnotations;

namespace HMS_API.Models.Dto
{
    public class AddTestDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
