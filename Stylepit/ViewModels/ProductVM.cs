using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stylepit.Models;

namespace Stylepit.ViewModels
{
    public class ProductVM
    {
        public virtual List<Category> Categories { get; set; }
        public virtual List<Size> Sizes { get; set; }
        public virtual List<Brand> Brands { get; set; }
        public virtual Product product { get; set; }
        public virtual List<Product> ProductsOnSale { get; set; }
        public virtual List<Product> ProductsNew { get; set; }
        public virtual List<SizeProduct> SizeProducts { get; set; }


        public virtual Cart Cart { get; set; }
    }
}