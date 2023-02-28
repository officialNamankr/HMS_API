using Microsoft.AspNetCore.Identity;

namespace HMS_API.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; }
        public DateTime Addedon { get; set; }
    }
}
