using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Tables
{
    public class Product
    {
        [Key]
        public int Id { get; set; } 
        public string Name { get; set; }

        public decimal Price { get; set; }
        public float DiscountInPercent { get; set; } = 0;


        public decimal? DiscountedPrice { get { return Price - ((decimal)DiscountInPercent * Price) / 100; } }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        
    }
}
