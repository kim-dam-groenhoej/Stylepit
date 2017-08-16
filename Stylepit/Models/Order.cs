using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stylepit.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNr { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }
        public string Country { get; set; }

        public bool IsVAT { get; set; } = true;
        public bool IsConfirmed { get; set; } = false;

        public DateTime DateOrder { get; set; } = DateTime.Now;
        public int CartId { get; set; }
        public virtual Cart Cart { get; set; }

    }
}