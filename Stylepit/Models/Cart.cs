using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stylepit.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; }


        public bool IsActive { get; set; } = true;
    }
}