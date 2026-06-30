using Dars_5ososy_API.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dars_5ososy_API.Infrastructure
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<long>, long>
    {
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Governorate> Governorates { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<SlotAddress> SlotAddresses { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<TeacherSubject> TeacherSubjects { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<AvailabilitySlot> AvailabilitySlots { get; set; }
        public DbSet<EducationSystem> EducationSystems { get; set; }
        public DbSet<EducationStage> EducationStages { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Image> Images { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users");
            builder.Entity<IdentityRole<long>>().ToTable("Roles");
            builder.Entity<IdentityUserRole<long>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<long>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<long>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<long>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<long>>().ToTable("UserTokens");

            builder.Entity<User>().ToTable("Users")
                .Ignore(u => u.TwoFactorEnabled);

            builder.Entity<TeacherSubject>()
                .HasKey(e => new { e.TeacherId, e.SubjectId });
            
            builder.Entity<Favorite>()
                .HasKey(e => new { e.StudentId, e.TeacherId });

            builder.Entity<Favorite>()
                .HasOne(f => f.Student)
                .WithMany()
                .HasForeignKey(f => f.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Favorite>()
                .HasOne(f => f.Teacher)
                .WithMany()
                .HasForeignKey(f => f.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Review>()
                .HasOne(r => r.Student)
                .WithMany()
                .HasForeignKey(r => r.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Review>()
                .HasOne(r => r.Teacher)
                .WithMany()
                .HasForeignKey(r => r.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);

            new DbInitializer(builder).Seed();
        }
    }
}
