using Dars_5ososy_API.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dars_5ososy_API.Infrastructure.Seeders
{
    public static class RoleSeed
    {
        private static readonly List<string> Roles = new() { "Admin", "Teacher", "Student" };

        public static void Seed(ModelBuilder modelBuilder)
        {
            var roleId = 1;
            foreach (var role in Roles)
            {
                modelBuilder.Entity<IdentityRole<long>>().HasData(new IdentityRole<long>
                {
                    Id = roleId++,
                    Name = role,
                    NormalizedName = role.ToUpper()
                });
            }
        }
    }
}
