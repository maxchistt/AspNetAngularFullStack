using Backend.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Orders
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public bool IsCompleted { get; set; } = false;

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public User? Customer { get; set; }

        public ICollection<OrderLine>? OrderItems { get; set; }

        public decimal? TotalPrice() => OrderItems?.Aggregate<OrderLine, decimal?>(
            decimal.Zero,
            (acc, item) => acc is not null
                ? acc.Value + item.TotalPrice()
                : null
        );
    }
}