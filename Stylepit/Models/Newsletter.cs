using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stylepit.Models
{
    public class Newsletter
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string HTMLContent { get; set; }
    }
}