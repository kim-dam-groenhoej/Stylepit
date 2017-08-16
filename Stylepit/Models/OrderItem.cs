using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stylepit.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public string Size { get; set; }
        public int Amount { get; set; }

        public decimal Price { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public decimal TotalPrice => Amount * Price;

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}