using Dars_5ososy_API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dars_5ososy_API.Infrastructure.Seeders
{
    public static class EducationSystemSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EducationSystem>().HasData(
                new EducationSystem
                {
                    Id = 1,
                    ArabicName = "النظام القديم",
                    EnglishName = "Old System"
                },
                new EducationSystem
                {
                    Id = 2,
                    ArabicName = "النظام الحديث",
                    EnglishName = "Modern System"
                },
                new EducationSystem
                {
                    Id = 3,
                    ArabicName = "نظام البكالوريا",
                    EnglishName = "Baccalaureate System"
                }
            );
        }
    }
}
