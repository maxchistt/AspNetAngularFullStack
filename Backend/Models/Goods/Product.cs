using Backend.Models.Orders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Goods
{
    public class Product
    {
        [Key, Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; } = null;

        [Required]
        public decimal Price { get; set; } = decimal.Zero;

        [Required]
        public int CategoryId { get; set; } = 0;

        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }

        [Required]
        public ProductInventory? Inventory { get; set; }

        public ICollection<OrderLine>? OrderItems { get; set; }
    }
}