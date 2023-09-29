using Backend.Auth.Services.Interfaces;
using Backend.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Backend.EF.Context
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
            IPasswordHashingService hasher = Database.GetService<IPasswordHashingService>()
                ?? throw new NullReferenceException("No PasswordHashingService!");

            modelBuilder.Entity<User>().HasData(InitialData.Users.Select(user => new User()
            {
                Id = user.Id,
                Email = user.Email,
                Password = hasher.HashPassword(user.Password),
                Role = user.Role
            }));
        }
    }
}