using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllAboutWeezer.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }



        [NotMapped]
        public IList<string>? RoleNames { get; set; }
    }
}
