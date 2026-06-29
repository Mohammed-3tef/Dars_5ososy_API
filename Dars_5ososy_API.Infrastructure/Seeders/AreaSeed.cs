using Dars_5ososy_API.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dars_5ososy_API.Infrastructure.Seeders
{
    public static class AreaSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>().HasData(
                new Area { Id = 1, ArabicName = "Area 1", EnglishName = "Area 1", GovernorateId = 1 }
            );
        }
    }
}
