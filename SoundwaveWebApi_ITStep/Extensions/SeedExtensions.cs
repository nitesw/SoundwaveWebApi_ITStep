using Core.Models;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace SoundwaveWebApi_ITStep.Extensions
{
    public static class Seeder
    {
        public static async Task SeedRoles(this IServiceProvider app)
        {
            var roleManager = app.GetRequiredService<RoleManager<IdentityRole>>();
    
            var roles = typeof(Roles).GetFields(
                BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Select(x => (string)x.GetValue(null)!);
    
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    
        public static async Task SeedAdmin(this IServiceProvider app, IConfiguration configuration)
        {
            var userManager = app.GetRequiredService<UserManager<User>>();
    
            string USERNAME = configuration.GetSection("AdminInfo").GetValue<string>("Username")!;
            string EMAIL = configuration.GetSection("AdminInfo").GetValue<string>("Email")!;
            string PASSWORD = configuration.GetSection("AdminInfo").GetValue<string>("Password")!;
    
            var existingUser = await userManager.FindByNameAsync(USERNAME);
    
            if (existingUser == null)
            {
                var user = new User
                {
                    UserName = USERNAME,
                    Email = EMAIL,
                    EmailConfirmed = true
                };
    
                var result = await userManager.CreateAsync(user, PASSWORD);
    
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(user, Roles.ADMIN);
            }
        }
    }
}
