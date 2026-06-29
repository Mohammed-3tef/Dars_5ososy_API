using Dars_5ososy_API.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dars_5ososy_API.Infrastructure.Seeders
{
    public static class AdminSeed
    {
        public const string AdminEmail = "mohammed.atef.abdelkader@gmail.com";

        public static void Seed(ModelBuilder modelBuilder)
        {
            var hasher = new PasswordHasher<User>();

            var admin = new User
            {
                Id = 1,
                UserName = "admin",
                FirstName = "System",
                LastName = "Administrator",
                Gender = 'M',
                BirthDate = new DateOnly(1990, 1, 1),
                NormalizedUserName = "ADMIN",
                Email = AdminEmail,
                NormalizedEmail = AdminEmail.ToUpper(),
                EmailConfirmed = true,
            };

            admin.PasswordHash = hasher.HashPassword(admin, "Admin@123");

            modelBuilder.Entity<User>().HasData(admin);
            modelBuilder.Entity<IdentityUserRole<long>>().HasData(
                new IdentityUserRole<long>
                {
                    UserId = admin.Id,
                    RoleId = 1
                }
            );
        }
    }
}
