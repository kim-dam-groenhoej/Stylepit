using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stylepit.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; }

        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}