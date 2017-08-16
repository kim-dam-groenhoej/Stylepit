using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stylepit.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public string CardAddressCountry { get; set; }
        public string CardAddressLine1 { get; set; }
        public string CardAddressCity { get; set; }
        public string CardAddressZip { get; set; }

        public string CardCVC { get; set; }
        public int CardExpirationMonth { get; set; }
        public int CardExpirationYear { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
    }
}