using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;
using FinalProject.ViewModel;

namespace FinalProject.Controllers
{
    public class SaleController : Controller
    {

        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public SaleController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Sale(SaleVM saleVM)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("login", "account");
            }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            Sale sale = new Sale
            {
                NameAndSurname = saleVM.NameAndSurname,
                Email = saleVM.Email,
                Address = saleVM.Address,
                City = saleVM.City,
                Country = saleVM.Country,
                CardNameAndSurname = saleVM.CardNameAndSurname,
                CardNumber = saleVM.CardNumber,
                ExpMonth = saleVM.ExpMonth,
                ExpYEAR = saleVM.ExpYear,
                Cvv = saleVM.Cvv,

            };

            sale.AppUserId = user.Id;
            var basket = Request.Cookies["basket"];

            if (basket == null) return NotFound();


            var products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);

            double total = 0;
            foreach (var product in products)
            {

                if (product.IsBook == true)
                {
                    SaleBook saleBook = new();

                    saleBook.Price = product.Price;
                    saleBook.BookId = product.Id;
                    saleBook.SaleId = sale.Id;
                    sale.SaleBooks.Add(saleBook);

                    if (product.DiscountPrice==null)
                    {
                        total += product.Price;
                    }
                    else
                    {

                        total += (double)product.DiscountPrice;
                    }
                }
                else
                {
                    SaleProduct saleProduct = new();


                    saleProduct.Price = product.Price;
                    saleProduct.ProductId = product.Id;
                    saleProduct.SaleId = sale.Id;
                    sale.SaleProducts.Add(saleProduct);
                    if (product.DiscountPrice == null)
                    {
                        total += product.Price;
                    }
                    else
                    {

                        total += (double)product.DiscountPrice;
                    }

                }

                
            }
            sale.Total = total;
            sale.CreatedAt = DateTime.Now;
            _context.Sales.Add(sale);
            _context.SaveChanges();
            Response.Cookies.Delete("basket");


            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("mammadova.nazrin2004@gmail.com", "BookCneter");
            mail.To.Add(new MailAddress(user.Email));
            mail.Subject = "sifariş qeydi ";
            mail.Body = $"<p> Sifarisiniz qeydə alindi... </p>";
            mail.IsBodyHtml = true;
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("mammadova.nazrin2004@gmail.com", "qoec hyzg wlwv rdqf");
            smtpClient.Send(mail);




            return RedirectToAction("index", "home");


        }
    }
}
