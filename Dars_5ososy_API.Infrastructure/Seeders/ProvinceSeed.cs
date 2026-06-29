using Dars_5ososy_API.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dars_5ososy_API.Infrastructure.Seeders
{
    public static class ProvinceSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Province>().HasData(
                new Province { Id = 1, ArabicName = "القاهرة الكبرى", EnglishName = "Great Cairo" },
                new Province { Id = 2, ArabicName = "الاسكندرية", EnglishName = "Alexandria" },
                new Province { Id = 3, ArabicName = "الدلتا", EnglishName = "Al-Delta" },
                new Province { Id = 4, ArabicName = "القناة", EnglishName = "Al-Canal" },
                new Province { Id = 5, ArabicName = "شمال الصعيد", EnglishName = "North of Al-Saieed" },
                new Province { Id = 6, ArabicName = "وسط الصعيد", EnglishName = "Middle of Al-Saieed" },
                new Province { Id = 7, ArabicName = "جنوب الصعيد", EnglishName = "South of Al-Saieed" }
            );
        }
    }
}
