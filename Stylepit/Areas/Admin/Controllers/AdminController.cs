using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stylepit.Models;
using Stylepit.ViewModels;
using System.IO;
using System.Text;
using System.Net.Mail;

namespace Stylepit.Areas.Admin.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin

        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        #region Products
        public ActionResult ProductList()
        {
            ProductVM vm = new ProductVM()
            {
                Categories = db.Categories.ToList(),
                SizeProducts = db.SizeProducts.ToList(),
            };
            return View(vm);
        }
        [HttpGet]
        public ActionResult ProductEdit(int? Id)
        {
            ProductVM vm = new ProductVM()
            {
                Categories = db.Categories.ToList(),
                Sizes = db.Sizes.ToList(),
                product = db.Products.Find(Id)
            };
            return View(vm);
        }
        [HttpPost]
        public ActionResult ProductEdit(int Id, string Title, string Description, decimal Price, int Amount, string OnSale)
        {
            Product prod = db.Products.Find(Id);
            prod.Title = Title;
            prod.Description = Description;
            prod.Price = Price;
            prod.InStock = false;
            if (Amount > 0)
            {
                prod.InStock = true;
            }
            prod.Amount = Amount;
            prod.OnSale = false;
            if (OnSale != null)
            {
                prod.OnSale = true;
            }


            db.SaveChanges();


            return RedirectToAction("ProductList");
        }
        [HttpGet]
        public ActionResult ProductCreate()
        {
            ProductVM vm = new ProductVM()
            {
                Categories = db.Categories.ToList(),
                Sizes = db.Sizes.ToList(),
                Brands = db.Brands.ToList()
            };

            return View(vm);
        }
        [HttpPost]
        public ActionResult ProductCreate(string Title, string Description, decimal Price, int Amount, string OnSale, int CategoryId, int[] SizeId, int BrandId, HttpPostedFileBase file, IEnumerable<HttpPostedFileBase> files)
        {
            Product prod = new Product()
            {
                Title = Title,
                Description = Description,
                Price = Price,
                InStock = false,
                CategoryId = CategoryId,
                ThumbImg = file.FileName,
                BrandId = BrandId
            };

            if (Amount > 0)
            {
                prod.InStock = true;
            }
            prod.Amount = Amount;
            prod.OnSale = false;
            if (OnSale == "on")
            {
                prod.OnSale = true;
            }

            db.Products.Add(prod);
            db.SaveChanges();
            foreach (var item in files)
            {
                if (item != null && item.ContentLength > 0)
                {
                    var filename = Path.GetFileName(item.FileName);

                    var path = Path.Combine(Server.MapPath("/Content/images/"), filename);
                    item.SaveAs(path);


                    ProductImage image = new ProductImage()
                    {
                        ImgUrl = filename,
                        ProductId = prod.Id
                    };
                    db.ProductImages.Add(image);
                    db.SaveChanges();
                }
            }
            if (file != null && file.ContentLength > 0)
            {
                var filename = Path.GetFileName(file.FileName);

                var path = Path.Combine(Server.MapPath("/Content/images/"), filename);
                file.SaveAs(path);
            }

            foreach (var item in SizeId)
            {
                SizeProduct size = new SizeProduct()
                {
                    SizeId = item,
                    ProductId = prod.Id
                };
                db.SizeProducts.Add(size);
                db.SaveChanges();
            }

            return RedirectToAction("ProductList");
        }
        public ActionResult ProductDelete(int Id)
        {
            Product prod = db.Products.Find(Id);

            db.Products.Remove(prod);
            db.SaveChanges();


            return RedirectToAction("ProductList");
        }
        #endregion

        #region Categories
        public ActionResult CategoryList()
        {
            List<Category> allCategories = db.Categories.ToList();
            return View(allCategories);
        }
        public ActionResult CategoryCreate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CategoryCreate(string Title)
        {
            Category cat = new Category()
            { Title = Title };
            db.Categories.Add(cat);
            db.SaveChanges();

            return RedirectToAction("CategoryList");
        }
        #endregion

        #region Sizes
        public ActionResult SizeList()
        {
            List<Size> allSizes = db.Sizes.ToList();
            return View(allSizes);
        }
        public ActionResult SizeCreate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SizeCreate(string Title)
        {
            Size size = new Size()
            { Title = Title };
            db.Sizes.Add(size);
            db.SaveChanges();

            return RedirectToAction("SizeList");
        }
        #endregion

        #region Newsletter

        [HttpGet]
        public ActionResult NewsletterList()
        {
            List<Newsletter> allNewsletters = db.Newsletters.ToList();

            return View(allNewsletters);
        }
        [HttpGet]
        public ActionResult NewsletterCreate()
        {
            //List<Newsletter> allNewsletters = db.Newsletters.ToList();

            return View();
        }
        [HttpPost]

        [ValidateInput(false)]

        public ActionResult NewsletterCreate(string Title, string HTMLContent)
        {

            Newsletter newsletter = new Newsletter()
            {
                Title = Title,
                HTMLContent = HTMLContent
            };
            db.Newsletters.Add(newsletter);
            db.SaveChanges();

            return RedirectToAction("ProductList");
        }
        [HttpGet]
        public ActionResult NewsletterSend(int? Id)
        {
            

            NewsletterVM vm = new NewsletterVM()
            {
                Newsletter = db.Newsletters.Find(Id),
                Subscribers = db.Subscribers.ToList()
            }
;
            return View(vm);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NewsletterSend(string[] Email, string title, string HTMLContent)
        {
            foreach (var item in Email)
            {
                MailMessage message = new MailMessage();

                message.From = new MailAddress("rben@techcollege.dk");
                message.To.Add(new MailAddress(item));

                message.IsBodyHtml = true;
                message.BodyEncoding = Encoding.UTF8;
                message.Subject = title;
                message.Body = HTMLContent;

                SmtpClient client = new SmtpClient();
                client.Send(message);
            }

            return RedirectToAction("NewsletterList");
        }
        #endregion
    }
}