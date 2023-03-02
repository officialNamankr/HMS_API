using System.ComponentModel.DataAnnotations;

namespace HMS_API.Models.Dto.GetDtos
{
    public class LoginViewDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
