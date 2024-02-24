using AllAboutWeezer.Models;
using AllAboutWeezer.Data;
using Microsoft.AspNetCore.Identity;

namespace AllAboutWeezer
{
    public class SeedData
    {
        public static void Seed(AppDbContext context, IServiceProvider provider)
        {
          
                var userManager = provider.GetRequiredService<UserManager<AppUser>>();
                var user1 = new AppUser { Name = "Orion Hill", UserName = "SeededOhill17" };
                var user2 = new AppUser { Name = "Savannah Slaney", UserName = "SeededSavannah" };
                const string SECRET_PASSWORD = "P@22word";
                bool isSuccess = userManager.CreateAsync(user1, SECRET_PASSWORD).Result.Succeeded;
                isSuccess &= userManager.CreateAsync(user2, SECRET_PASSWORD).Result.Succeeded;

                context.SaveChanges();
            
        }
    }
}
