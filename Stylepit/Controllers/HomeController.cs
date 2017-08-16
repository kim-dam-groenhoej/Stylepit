using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stylepit.Models;
using Stylepit.ViewModels;
using MvcBreadCrumbs;

namespace Stylepit.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [BreadCrumb(Label = "Forside")]
        public ActionResult Index()
        {
            var mydate = DateTime.Today.AddDays(-4);
            ProductVM vm = new ProductVM()
            {
                ProductsOnSale = db.Products.Where(x => x.OnSale == true).ToList(),
                ProductsNew = db.Products.Where(x => x.DateCreated >= mydate).ToList()
            };

            return View(vm);
        }
        [HttpPost]
        public void SignUpNewsletter(string name, string email)
        {
            Subscriber sub = new Subscriber()
            {
                Name = name,
                Email = email,
            };
            db.Subscribers.Add(sub);
            db.SaveChanges();
        }



    }
}