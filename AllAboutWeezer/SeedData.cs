using AllAboutWeezer.Data;
using AllAboutWeezer.Models;
using Microsoft.AspNetCore.Identity;

namespace AllAboutPigeons
{
    public class SeedData
    {
        public static void Seed(AppDbContext context, IServiceProvider provider)
        {
            if (!context.Message.Any())
            {
                var userManager = provider.GetRequiredService<UserManager<SignUp>>();
                var user1 = new SignUp { Name = "Orion Hill", UserName = "Ohill17" };
                var user2 = new SignUp { Name = "Savannah Slaney", UserName = "Savannah" };
                const string SECRET_PASSWORD = "P@ssword";
                userManager.CreateAsync(user1, SECRET_PASSWORD);
                userManager.CreateAsync(user2, SECRET_PASSWORD);

                context.SaveChanges();
            }
        }
    }
}
