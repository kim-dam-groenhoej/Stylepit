using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stylepit.Models
{
    public class Product
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public bool InStock
        {
            get
            {
                return this.Amount > 0;
            }
        }

        public int Amount { get; set; }
        public bool? OnSale { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string ThumbImg { get; set; }

        public int? BrandId { get; set; }
        public virtual Brand Brand { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<Size> Sizes { get; set; }
    }
}