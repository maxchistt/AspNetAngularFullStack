﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Goods
{
    public class ProductInventory
    {
        [ForeignKey(nameof(Product)), Key]
        public int ProductId { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public Product? Product { get; set; }
    }
}