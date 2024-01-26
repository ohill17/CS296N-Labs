using Microsoft.AspNetCore.Identity;

namespace AllAboutWeezer.Models
{
    public class SignUp : IdentityUser
    {
        public string Name { get; set; }
    }
}
