using Microsoft.AspNetCore.Identity;

namespace AllAboutWeezer.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
