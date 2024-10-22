using System.Text.Json;
using datingapp_api.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {

        public static async Task<bool> SeedUsers(UserManager<User> userManager, 
            RoleManager<Role> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return false;

            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

            var users = JsonSerializer.Deserialize<List<User>>(userData);

            if (users == null) return false;

            var roles = new List<Role>()
            {
                new Role{Name = "Member"},
                new Role{Name = "Admin"},
                new Role{Name = "Moderator"}
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var user in users)
            {

                user.UserName = user.UserName.ToLower();
                await userManager.CreateAsync(user, "P@ssWord123");
                await userManager.AddToRoleAsync(user, "Member");
            }

            var admin = new User
            {
                UserName = "admin",
                City = "admin",
                Country = "admin",
                Gender = "male",
                KnownAs = "Admin",
                
            };

            await userManager.CreateAsync(admin, "P@ssWord123");
            await userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator" });

            return true;
        }
    }
}