using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using datingapp_api.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {

        public static async Task<bool> SeedUsers(DatingAppContext context)
        {
            if (await context.Users.AnyAsync()) return false;

            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

            var users = JsonSerializer.Deserialize<List<User>>(userData);

            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("P@ssWord123"));
                user.PasswordSalt = hmac.Key;
                context.Users.Add(user);
                
            }
            await context.SaveChangesAsync();
            return true;
        }
    }
}