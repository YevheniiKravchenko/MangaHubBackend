using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace DAL.DbContexts
{
    public class DbContextBase : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Person> Persons { get; set; } = null!;

        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

        public DbContextBase(DbContextOptions<DbContextBase> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public void Commit()
        {
            SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MapUser(modelBuilder);

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
                .HasOne(u => u.Person)
                .WithOne(p => p.User)
                .HasForeignKey<Person>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Person>()
                .HasKey(p => p.UserId);

            modelBuilder.Entity<User>()
                .HasMany(x => x.RefreshTokens)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RefreshToken>()
                .Property(x => x.RefreshTokenId)
                .HasValueGenerator(typeof(GuidValueGenerator));
        }
    }
}
