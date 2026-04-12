using DeStaProduction.Infrastucture.Entities;
using Microsoft.AspNetCore.Identity;

namespace DeStaProduction.Seed
{
    public class IdentitySeeder
    {
        public static async Task SeedRoldesAsync(RoleManager<IdentityRole<Guid>> roleManager,UserManager<DeStaUser> userManager)
        {
          
            string[] roles = { "Admin", "Artist", "User"};

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }

 
            var adminEmail = "admin@desta.com";
            var adminPassword = "Admin123!";

            var admin = await userManager.FindByEmailAsync(adminEmail);

            if (admin == null)
            {
                admin = new DeStaUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Атанас",
                    LastName = "Хътов",
                    EmailConfirmed = true,
                    IsApproved = true
                };

                await userManager.CreateAsync(admin, adminPassword);
                await userManager.AddToRoleAsync(admin, "Admin");
            }

 
            var artistEmail = "Sisi@desta.com";

            var artist = await userManager.FindByEmailAsync(artistEmail);

            if (artist == null)
            {
                artist = new DeStaUser
                {
                    UserName = artistEmail,
                    Email = artistEmail,
                    FirstName = "Силвия",
                    LastName = "Качакова",
                    EmailConfirmed = true,
                    IsApproved = true
                };

                await userManager.CreateAsync(artist, "Sisi123!");
                await userManager.AddToRoleAsync(artist, "Artist");
            }
            var artistEmail2 = "Qna@desta.com";

            var artist2 = await userManager.FindByEmailAsync(artistEmail2);

            if (artist2 == null)
            {
                artist2 = new DeStaUser
                {
                    UserName = artistEmail2,
                    Email = artistEmail2,
                    FirstName = "Яна",
                    LastName = "Огнянова",
                    EmailConfirmed = true,
                    IsApproved = true
                };

                await userManager.CreateAsync(artist2, "Qna123!");
                await userManager.AddToRoleAsync(artist2, "Artist");
            }

            var userEmail = "Stelko@desta.com";

            var user = await userManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                user = new DeStaUser
                {
                    UserName = userEmail,
                    Email = userEmail,
                    FirstName = "Стелко",
                    LastName = "Чанев",
                    EmailConfirmed = true,
                    IsApproved = true
                };

                await userManager.CreateAsync(user, "Stelko123!");
                await userManager.AddToRoleAsync(user, "User");
            }
        }
    }
}