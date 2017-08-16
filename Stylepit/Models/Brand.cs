using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stylepit.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}