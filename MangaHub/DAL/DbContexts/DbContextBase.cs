using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace DAL.DbContexts
{
    public class DbContextBase : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<UserProfile> Profiles { get; set; } = null!;

        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

        public DbSet<ResetPasswordToken> ResetPasswordTokens { get; set; } = null!;

        public DbSet<Manga> Mangas { get; set; } = null!;

        public DbSet<Chapter> Chapters { get; set; } = null!;

        public DbSet<Rating> Ratings { get; set; } = null!;

        public DbSet<Comment> Comments { get; set; } = null!;

        public DbContextBase(DbContextOptions<DbContextBase> options)
            : base(options)
        {
        }

        public void Commit()
        {
            SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MapUser(modelBuilder);
            MapManga(modelBuilder);
            AddAdmin(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void MapUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<User>()
                .Property(u => u.UserId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                .HasOne(u => u.UserProfile)
                .WithOne(p => p.User)
                .HasForeignKey<UserProfile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserProfile>()
                .HasKey(p => p.UserId);

            modelBuilder.Entity<User>()
                .HasMany(x => x.RefreshTokens)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RefreshToken>()
                .Property(x => x.RefreshTokenId)
                .HasValueGenerator(typeof(GuidValueGenerator));

            modelBuilder.Entity<User>()
                .HasMany(x => x.ResetPasswordTokens)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ResetPasswordToken>()
                .Property(x => x.ResetPasswordTokenId)
                .HasValueGenerator(typeof(GuidValueGenerator));

            modelBuilder.Entity<User>()
                .HasMany(u => u.Ratings)
                .WithOne(r => r.User)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Mangas)
                .WithOne(m => m.User)
                .OnDelete(DeleteBehavior.SetNull);
        }

        public void MapManga(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manga>()
                .Property(m => m.MangaId)
                .HasValueGenerator(typeof(GuidValueGenerator));

            modelBuilder.Entity<Chapter>()
                .Property(c => c.ChapterId)
                .HasValueGenerator(typeof(GuidValueGenerator));

            modelBuilder.Entity<Rating>()
                .Property(c => c.RatingId)
                .HasValueGenerator(typeof(GuidValueGenerator));

            modelBuilder.Entity<Manga>()
                .HasMany(m => m.Chapters)
                .WithOne(c => c.Manga)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Manga>()
                .HasMany(m => m.Ratings)
                .WithOne(r => r.Manga)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void AddAdmin(ModelBuilder modelBuidler)
        {
            modelBuidler.Entity<User>().HasData(new User
            {
                UserId = 1,
                Login = "Admin",
                PasswordHash = "$2a$10$WkrWKFdubfRwcY4MjdFELui7Dh8r3ykAvDYOQPvQud0vPlxFHVen.", // password: admin231_rte
                PasswordSalt = "d!W2~4~zI{wq:l<p",
                RegistrationDate = DateTime.UtcNow,
                IsAdmin = true
            });

            modelBuidler.Entity<UserProfile>().HasData(new UserProfile
            {
                UserId = 1,
                FirstName = "Admin",
                LastName = "Admin",
                Avatar = Array.Empty<byte>(),
                Description = "Main administrator of the service",
                PhoneNumber = "0505050505",
                ShowConfidentialInformation = false,
                BirthDate = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                Email = "admin@mangahub.com"
            });
        }
    }
}
