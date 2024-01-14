using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.Services;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;

namespace FinalProject.Controllers
{
    public class BasketController : Controller
    {
        int id = 0;
        private readonly AppDbContext _context;
        private readonly IBasketService _basketService;
        private readonly UserManager<AppUser> _userManager;

        public BasketController(AppDbContext context, IBasketService basketService, UserManager<AppUser> userManager)
        {
            _context = context;
            _basketService = basketService;
            _userManager = userManager;
        }

        public IActionResult AddBook(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("login", "account");
            }
            if (id == null) return NotFound();
            var product = _context.Books

                .FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            var products = CheckBasket();
            CheckBookInBasket(products, product.Id);


            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products));
            return RedirectToAction("ShowBasket", "Basket");

        }
        public IActionResult AddProduct(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("login", "account");
            }
            if (id == null) return NotFound();
            var product = _context.Products

                .FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            var products = CheckBasket();
            CheckProductInBasket(products, product.Id);


            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products));
            return RedirectToAction("ShowBasket", "Basket");

        }
        public IActionResult ShowBasket()
        {

            string basket = Request.Cookies["basket"];
            List<BasketVM> products = new();
            if (basket == null) return View(products);

            products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            foreach (var basketProduct in products)
            {
                if (basketProduct.IsBook==true)
                {
                    var dbProduct = _context.Books
                  .FirstOrDefault(p => p.Id == basketProduct.Id);
                    basketProduct.Name = dbProduct.Name; 
                    basketProduct.Price = dbProduct.Price;
                    basketProduct.DiscountPrice = dbProduct.DiscountPrice;

                    basketProduct.ImageUrl = dbProduct.ImgUrl;
                }
                else
                {
                    var dbProduct = _context.Products
                  .FirstOrDefault(p => p.Id == basketProduct.Id);
                    basketProduct.Name = dbProduct.Name;
                    basketProduct.Price = dbProduct.Price;
                    basketProduct.DiscountPrice = dbProduct.DiscountPrice;
                    basketProduct.ImageUrl = dbProduct.ImgUrl;
                }
            }

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products));
            return View(products);


        }

        public IActionResult DeleteBook(int? id)
        {
            if (id == null)
                return NotFound();

            _basketService.DeleteBook(id.Value);

            return RedirectToAction("ShowBasket");

        }
        public IActionResult DeleteProduct(int? id)
        {
            if (id == null)
                return NotFound();

            _basketService.DeleteProduct(id.Value);

            return RedirectToAction("ShowBasket");

        }
        public IActionResult IncreaseBook(int id)
        {
            _basketService.IncreaseBook(id);

            return RedirectToAction("ShowBasket");
        }
        public IActionResult IncreaseProduct(int id)
        {
            _basketService.IncreaseProduct(id);

            return RedirectToAction("ShowBasket");
        }


        public IActionResult DecreaseBook(int id)
        {
            _basketService.DecreaseBook(id);
            return RedirectToAction("ShowBasket");


        }
        public IActionResult DecreaseProduct(int id)
        {
            _basketService.DecreaseProduct(id);
            return RedirectToAction("ShowBasket");

        }
        private List<BasketVM> CheckBasket()
        {
            List<BasketVM> products;
            string basket = Request.Cookies["basket"];
            if (basket == null)
            {
                products = new List<BasketVM>();

            }
            else
            {
                products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);


            }
            return products;
        }

        private void CheckBookInBasket(List<BasketVM> products, int id)
        {
            var existProduct = products.FirstOrDefault(p => p.Id == id&&p.IsBook==true);
            if (existProduct == null)
            {
                BasketVM basketVM = new();
                basketVM.Id = id;
                basketVM.BasketCount = 1;
                basketVM.IsBook = true;
                products.Add(basketVM);
            }
            else
            {
                existProduct.BasketCount++;
            }

        }
        private void CheckProductInBasket(List<BasketVM> products, int id)
        {
            var existProduct = products.FirstOrDefault(p => p.Id == id && p.IsBook == false);
            if (existProduct == null)
            {
                BasketVM basketVM = new();
                basketVM.Id = id;
                basketVM.BasketCount = 1;
                basketVM.IsBook= false;
                products.Add(basketVM);
            }
            else
            {
                existProduct.BasketCount++;
            }

        }

        //public async Task<IActionResult> Sale()
        //{
        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("login", "account");
        //    }
        //    var user = await _userManager.FindByNameAsync(User.Identity.Name);
        //    Sale sale = new();

        //    sale.AppUserId = user.Id;
        //    var basket = Request.Cookies["basket"];

        //    if (basket == null) return NotFound();


        //    var products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);

        //    double total = 0;
        //    foreach (var product in products)
        //    {

        //        SaleProduct saleProduct = new();
        //        saleProduct.Price = product.Price;
        //        saleProduct.ProductId = product.Id;
        //        saleProduct.SaleId = sale.Id;
        //        total += product.Price * product.BasketCount;

        //        sale.SaleProducts.Add(saleProduct);



        //    }
        //    sale.Total = total;
        //    sale.CreatedAt = DateTime.Now;
        //    _context.Sales.Add(sale);
        //    _context.SaveChanges();
        //    TempData["Success"] = "Ugurlu satish";


        //    return RedirectToAction("showbasket");


        //}
    }
}
