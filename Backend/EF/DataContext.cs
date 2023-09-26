using Backend.Auth.Models;
using Backend.Auth.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Backend.EF
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            bool created = Database.EnsureCreated();
            Console.WriteLine($"{nameof(DataContext)} connection created. EnsureCreated:{created}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            IPasswordHashingService hasher = this.Database.GetService<IPasswordHashingService>()
                ?? throw new NullReferenceException("No PasswordHashingService!");

            modelBuilder.Entity<User>().HasData(InitialData.Users.Select(user => new User(
                user.Email,
                hasher.HashPassword(user.Password),
                user.Role
            )));
        }
    }
}