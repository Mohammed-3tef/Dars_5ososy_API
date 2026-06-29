using Dars_5ososy_API.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;

namespace Dars_5ososy_API.Infrastructure
{
    public class DbInitializer
    {
        private readonly ModelBuilder _modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            ProvinceSeed.Seed(_modelBuilder);
            GovernorateSeed.Seed(_modelBuilder);
            AreaSeed.Seed(_modelBuilder);
            RoleSeed.Seed(_modelBuilder);
            AdminSeed.Seed(_modelBuilder);
            EducationStageSeed.Seed(_modelBuilder);
            EducationSystemSeed.Seed(_modelBuilder);
        }
    }
}
