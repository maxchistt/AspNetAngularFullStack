using Backend.EF.Initial;
using Backend.Models.Goods;
using Backend.Models.Orders;
using Backend.Models.Users;
using Backend.Services.DAL.Users.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Backend.EF.Context
{
    public class DataContext : DbContext
    {
        // Users

        public DbSet<User> Users { get; set; } = null!;

        // Goods

        public DbSet<Product> Goods { get; set; } = null!;
        public DbSet<ProductInventory> GoodsInventories { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;

        // Orders

        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderLine> OrderLines { get; set; } = null!;

        //

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            bool created = base.Database.EnsureCreated();
            Console.WriteLine($"{nameof(DataContext)} connection created. EnsureCreated:{created}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            IPasswordHashingService hasher = base.Database.GetService<IPasswordHashingService>()
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