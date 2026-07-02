using Dars_5ososy_API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dars_5ososy_API.Infrastructure.Seeders
{
    public static class SubjectSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>().HasData(
                // المواد الأساسية والمشتركة
                new Subject { Id = 1, Name = "اللغة العربية", Code = "ARA", Description = "منهج اللغة العربية واكتساب المهارات اللغوية والنحوية" },
                new Subject { Id = 2, Name = "اللغة الإنجليزية", Code = "ENG", Description = "منهج اللغة الإنجليزية للاتصال والمحادثة والقواعد" },
                new Subject { Id = 3, Name = "الدراسات الاجتماعية", Code = "SOC", Description = "منهج الجغرافيا والتاريخ ومبادئ المواطنة" },
                new Subject { Id = 4, Name = "اللغة الفرنسية", Code = "FRN", Description = "منهج اللغة الأجنبية الثانية - الفرنسية" },
                new Subject { Id = 5, Name = "اللغة الألمانية", Code = "GER", Description = "منهج اللغة الأجنبية الثانية - الألمانية" },
                new Subject { Id = 6, Name = "اللغة الإيطالية", Code = "ITA", Description = "منهج اللغة الأجنبية الثانية - الإيطالية" },
                new Subject { Id = 7, Name = "التربية الدينية", Code = "REL", Description = "منهج التربية الدينية الإسلامية والمسيحية" },

                // مواد المدارس العربي (الحكومي والخاص عربي)
                new Subject { Id = 8, Name = "الرياضيات", Code = "MAT-AR", Description = "منهج الرياضيات والحساب باللغة العربية" },
                new Subject { Id = 9, Name = "العلوم", Code = "SCI-AR", Description = "منهج العلوم العامة باللغة العربية" },
                new Subject { Id = 10, Name = "الفيزياء", Code = "PHY-AR", Description = "منهج الفيزياء المتقدمة باللغة العربية" },
                new Subject { Id = 11, Name = "الكيمياء", Code = "CHE-AR", Description = "منهج الكيمياء والتفاعلات باللغة العربية" },
                new Subject { Id = 12, Name = "الأحياء", Code = "BIO-AR", Description = "منهج علم الأحياء والبيئة باللغة العربية" },
                new Subject { Id = 13, Name = "الجيولوجيا", Code = "GEO-AR", Description = "منهج الجيولوجيا وعلوم الأرض باللغة العربية" },

                // مواد مدارس اللغات (تجريبي، خاص لغات، دولي)
                new Subject { Id = 14, Name = "Math", Code = "MAT-EN", Description = "Mathematics and algebra syllabus in English" },
                new Subject { Id = 15, Name = "Science", Code = "SCI-EN", Description = "General Science syllabus in English" },
                new Subject { Id = 16, Name = "Physics", Code = "PHY-EN", Description = "Advanced Physics concepts and mechanics in English" },
                new Subject { Id = 17, Name = "Chemistry", Code = "CHE-EN", Description = "Organic and inorganic Chemistry syllabus in English" },
                new Subject { Id = 18, Name = "Biology", Code = "BIO-EN", Description = "Living organisms and Biology syllabus in English" },

                // المواد الأدبية للمراحل المتقدمة
                new Subject { Id = 19, Name = "التاريخ", Code = "HIS", Description = "منهج التاريخ والحضارات للمرحلة الثانوية" },
                new Subject { Id = 20, Name = "الجغرافيا", Code = "GEO", Description = "منهج الجغرافيا السياسية والطبيعية للمرحلة الثانوية" },
                new Subject { Id = 21, Name = "الفلسفة والمنطق", Code = "PHI", Description = "مبادئ التفكير الفلسفي والمنطق الصوري" },
                new Subject { Id = 22, Name = "علم النفس والاجتماع", Code = "PSY", Description = "دراسة السلوك الإنساني ومبادئ علم الاجتماع" }
            );
        }
    }
}
