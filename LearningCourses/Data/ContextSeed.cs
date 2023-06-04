using LearningCourses.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;


namespace LearningCourses.Data
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
           
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Администратор.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Преподаватель.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Студент.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Гость.ToString()));

        }
        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                Name = "Админ",
                Surname = "Админов",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word");   
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Администратор.ToString());
                }
            }
        }
    }
}
