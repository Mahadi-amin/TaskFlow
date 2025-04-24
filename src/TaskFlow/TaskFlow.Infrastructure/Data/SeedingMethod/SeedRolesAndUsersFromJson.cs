using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using TaskFlow.Infrastructure.Data.Dtos;
using TaskFlow.Infrastructure.Dtos;

namespace TaskFlow.Infrastructure.Data.SeedingMethod
{
    public class SeedRolesAndUsersFromJson
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var rolesData = await File.ReadAllTextAsync("..\\TaskFlow.Infrastructure\\Data\\SeedData\\roles.json");
            var usersData = await File.ReadAllTextAsync("..\\TaskFlow.Infrastructure\\Data\\SeedData\\users.json");

            var roles = JsonSerializer.Deserialize<List<RoleSeedModel>>(rolesData);
            var users = JsonSerializer.Deserialize<List<UserSeedModel>>(usersData);

            if (roles == null || users == null)
            {
                throw new InvalidOperationException("Failed to read role or user seed data.");
            }

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role.Name))
                {
                    await roleManager.CreateAsync(new IdentityRole(role.Name));
                }
            }

            foreach (var userSeed in users)
            {
                var user = await userManager.FindByEmailAsync(userSeed.Email);
                if (user == null)
                {
                    user = new IdentityUser
                    {
                        UserName = userSeed.Email,
                        Email = userSeed.Email,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(user, userSeed.Password);

                    if (result.Succeeded && userSeed.Roles != null)
                    {
                        foreach (var role in userSeed.Roles)
                        {
                            if (await roleManager.RoleExistsAsync(role))
                            {
                                await userManager.AddToRoleAsync(user, role);
                            }
                        }
                    }
                    else
                    {
                        var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                    }
                }
            }
        }
    }
}
