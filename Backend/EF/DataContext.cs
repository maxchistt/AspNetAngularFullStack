using Backend.Auth.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.EF
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public DataContext()
        {
            bool created = Database.EnsureCreated();
            Console.WriteLine($"{nameof(DataContext)} connection created. EnsureCreated:{created}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(InitialData.Users);
        }
    }
}