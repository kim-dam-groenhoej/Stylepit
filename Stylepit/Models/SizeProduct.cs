using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Stylepit.Models
{
    public class SizeProduct
    {
        [Key]
        [Column(Order = 1)]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        [Key()]
        [Column(Order = 2)]
        public int SizeId { get; set; }
        public virtual Size Size { get; set; }
    }
}