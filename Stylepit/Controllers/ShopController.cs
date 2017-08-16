using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stylepit.Models;
using Stylepit.ViewModels;
using Microsoft.AspNet.Identity;
using MvcBreadCrumbs;
using Stripe;
using System.Threading.Tasks;

namespace Stylepit.Controllers
{
    public class ShopController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        [BreadCrumb(Label = "Forside")]
        public ActionResult Index()
        {
            return View();
        }

        [BreadCrumb]
        public ActionResult ProductDetail(int? id)
        {
            ProductVM vm = new ProductVM()
            {
                product = db.Products.Find(id),
                Sizes = db.Sizes.ToList(),


            };
            BreadCrumb.SetLabel(vm.product.Title);
            return View(vm);
        }

        public void AddToCart(int amount, int productId, string size, decimal price)
        {

            if (User.Identity.IsAuthenticated)
            {
                string CurrentUser = User.Identity.GetUserId();


                OrderItem orderitem = new OrderItem()
                {
                    Amount = amount,
                    ProductId = productId,
                    Size = size,
                    Price = price,
                    UserId = CurrentUser

                };
                db.OrderItems.Add(orderitem);
                db.SaveChanges();

                //DETTE TJEKKER OM DER ER EN KURV OG 
                //HVIS DER IKKE IKKE ER SKAL DEN LAVE EN MED DET PRODUKT VI LIGE HAR LAGT I
                if (!db.Carts.Any(x => x.UserId == CurrentUser && x.IsActive == true))
                {
                    List<OrderItem> oitem = new List<OrderItem> { };
                    oitem.Add(orderitem);
                    Cart cart = new Cart()
                    {
                        UserId = CurrentUser,
                        OrderItems = oitem
                    };
                    db.Carts.Add(cart);
                    db.SaveChanges();
                }
                else
                {
                    Cart cart = db.Carts.FirstOrDefault(x => x.UserId == CurrentUser && x.IsActive == true);
                    cart.OrderItems.Add(orderitem);

                    db.SaveChanges();
                };

            }



        }

        public ActionResult GetCart()
        {
            string CurrentUser = User.Identity.GetUserId();
            Cart cart = db.Carts.FirstOrDefault(x => x.UserId == CurrentUser && x.IsActive == true);
            return PartialView("_Cart", cart);
        }
        public ActionResult ShowCart(int? id)
        {
            Cart cart = db.Carts.Find(id);
            return View(cart);
        }
        public void RemoveFromCart(int id)
        {
            OrderItem order = db.OrderItems.Find(id);

            db.OrderItems.Remove(order);
            db.SaveChanges();
        }

        public ActionResult Checkout()
        {
            string CurrentUser = User.Identity.GetUserId();
            Cart cart = db.Carts.FirstOrDefault(x => x.UserId == CurrentUser && x.IsActive == true);
            decimal b = 0;
            foreach (var item in cart.OrderItems)
            {
                b = b + item.TotalPrice;
            }

            ViewBag.TotalOrderPrice = b;

            return View(cart);
        }
        [HttpPost]
        public ActionResult Checkout(string firstName, string lastName, string email, string address, int zipCode, string phoneNr, string country, int CartId, string City, string cardNumber, int month, int year, string cvc)
        {

            Order order = new Order()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Address = address,
                ZipCode = zipCode,
                PhoneNr = phoneNr,
                Country = country,
                CartId = CartId
            };



            Payment payment = new Payment()
            {
                CardAddressLine1 = address,
                CardAddressCountry = country,
                CardAddressCity = City,
                CardAddressZip = Convert.ToString(zipCode),
                CardNumber = cardNumber,
                CardExpirationMonth = month,
                CardExpirationYear = year,
                CardName = firstName,
                CardCVC = cvc
            };

            db.Orders.Add(order);
            db.SaveChanges();

            var response = PayWithStripe(payment);

            if (response.Paid)
            {
                Cart cart = db.Carts.Find(CartId);

                cart.IsActive = false;
                order.IsConfirmed = true;

                db.SaveChanges();
                return RedirectToAction("DETVIRKER");

            }
            else
            {
                return RedirectToAction("DET FEJLER FOR JER");
            }

        }

        #region STRIPE

        public StripeCharge PayWithStripe(Payment model)
        {
            string CurrentUser = User.Identity.GetUserId();
            Cart cart = db.Carts.FirstOrDefault(x => x.UserId == CurrentUser && x.IsActive == true);
            decimal b = 0;
            string orderitemsstring = "";
            foreach (var item in cart.OrderItems)
            {
                b = b + item.TotalPrice;
                orderitemsstring = orderitemsstring + item.Product.Title;
            }
            
            var chargeOption = new StripeChargeCreateOptions()
            {
                Amount = Convert.ToInt32((b)*100),
                Shipping = null,
                Currency = "dkk",
                Capture = true,
                Description = orderitemsstring,
                SourceCard = new SourceCard
                {
                    Number = model.CardNumber,
                    ExpirationMonth = model.CardExpirationMonth,
                    ExpirationYear = model.CardExpirationYear,
                    AddressLine1 = model.CardAddressLine1,
                    AddressCity = model.CardAddressCity,
                    AddressZip = model.CardAddressZip,
                    Name = model.CardName,
                    Cvc = model.CardCVC
                }
            };
            var chargeService = new StripeChargeService();
            StripeCharge stripeCharge = null;

            try
            {
                stripeCharge = chargeService.Create(chargeOption);
            }
            catch (StripeException exception)
            {
            }

            return stripeCharge;
        }
        #endregion


    }
}