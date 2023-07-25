namespace Adaptations.Data.Seeding
{
    using System;
    using System.Threading.Tasks;
    using Adaptations.Common;
    using Adaptations.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class AdminUserSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            // Ensure that the Administrator role exists
            var adminRoleName = GlobalConstants.AdministratorRoleName;
            var adminRole = await roleManager.FindByNameAsync(adminRoleName);
            if (adminRole == null)
            {
                await roleManager.CreateAsync(new ApplicationRole { Name = adminRoleName });
            }

            var adminUsername = "peshooooo@abv.bg";
            var adminEmail = "peshooo@abv.bg";
            var adminPassword = "1234567";

            var adminUser = await userManager.FindByNameAsync(adminUsername);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminUsername,
                    Email = adminEmail,
                };

                await userManager.CreateAsync(adminUser, adminPassword);
            }

            // Assign the Administrator role to the administrator user if not already assigned
            if (!await userManager.IsInRoleAsync(adminUser, adminRoleName))
            {
                await userManager.AddToRoleAsync(adminUser, adminRoleName);
            }
        }
    }
}