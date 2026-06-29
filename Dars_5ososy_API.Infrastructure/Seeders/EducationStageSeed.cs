using Dars_5ososy_API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dars_5ososy_API.Infrastructure.Seeders
{
    public static class EducationStageSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EducationStage>().HasData(
                new EducationStage
                {
                    Id = 1,
                    ArabicName = "المرحلة الابتدائية",
                    EnglishName = "Primary Stage"
                },
                new EducationStage
                {
                    Id = 2,
                    ArabicName = "المرحلة الإعدادية",
                    EnglishName = "Preparatory Stage"
                },
                new EducationStage
                {
                    Id = 3,
                    ArabicName = "المرحلة الثانوية",
                    EnglishName = "Secondary Stage"
                },
                new EducationStage
                {
                    Id = 4,
                    ArabicName = "المرحلة الجامعية",
                    EnglishName = "University Stage"
                }
            );
        }
    }
}
