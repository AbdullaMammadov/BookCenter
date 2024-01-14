using FinalProject.DAL;
using FinalProject.Extension;
using FinalProject.Models;
using FinalProject.ViewModel.Admin.Product;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.Reflection;
///2
namespace FinalProject.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class ProductController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductController(AppDbContext context, IWebHostEnvironment webHostEnvironment, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var product = _context.Products.FirstOrDefault(c => c.Id == id);
            if (product == null) return NotFound();
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var products = _context.Products
                .Include(b => b.CategoryPro)

                .ToList();
            return View(products);
        }
        public IActionResult Create()
        {
         
            ViewBag.categories = new SelectList(_context.CategoryPros.ToList(), "Id", "Name");
            return View();

        }
        [Area("AdminArea")]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateAsync(CreateProductVM createProductVM)
        {
          


            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Photo", "bosh qoyma");
                return View();

            }
            if (createProductVM.Photo.IsImage())
            {

                ModelState.AddModelError("Photo", "only image");
                return View();
            }
            if (createProductVM.Photo.CheckImageSize(1000))
            {
                ModelState.AddModelError("Photo", "sekil boyukdur");
                return View();
            }

            Product newproduct = new();

            newproduct.ImgUrl = createProductVM.Photo.SaveImage(webHostEnvironment, "assets/img");

         
            newproduct.CategoryProId = createProductVM.CategoryId;
            newproduct.Price = createProductVM.Price;
            newproduct.DiscountPrice = createProductVM.DiscountPrice;
            newproduct.Name = createProductVM.Name;
            newproduct.AddedBy = createProductVM.AddedBy;
            newproduct.CreatedAt = DateTime.Now;
            _context.Products.Add(newproduct);
            _context.SaveChanges();
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                var link = Url.Action("Detail", "Product", new { Id = newproduct.Id }, Request.Scheme, Request.Host.ToString());
                var newLink = link.Replace("/AdminArea/", "/");
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("mammadova.nazrin2004@gmail.com", "Book Center");
                mail.To.Add(new MailAddress(user.Email));
                mail.Subject = "New Product";
                mail.Body = $"<a href={newLink}> Yeni Məsulumuzu görmək üçün linkə dahil olun</a>";
                mail.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential("mammadova.nazrin2004@gmail.com", "cpai qbgb ybst rmyk");
                smtpClient.Send(mail);


            }


            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();

            var product = _context.Products
                .Include(b => b.CategoryPro)
                .FirstOrDefault(c => c.Id == id);

            if (product == null) return NotFound();

            // Map the product properties to UpdateproductVM
            var updateproductVM = new UpdateProductVM
            {
                Id = product.Id,
                Name = product.Name,
             
                Photo = null, // You may need to handle the image update separately
                CategoryId = product.CategoryProId,
                Price = product.Price,
                DiscountPrice = product.DiscountPrice
            };

         
            ViewBag.categories = new SelectList(_context.CategoryPros.ToList(), "Id", "Name");

            return View(updateproductVM);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public IActionResult Update(UpdateProductVM updateproductVM)
        {
            if (!ModelState.IsValid) return View(updateproductVM);

            // Retrieve the existing product from the database
            var existingproduct = _context.Products
     
                .FirstOrDefault(c => c.Id == updateproductVM.Id);

            // If the product with the given ID doesn't exist, return a not found response
            if (existingproduct == null) return NotFound();

            // Update the product properties with the values from the updateproductVM
            existingproduct.Name = updateproductVM.Name;
        
        
            // Update other product properties
            existingproduct.CategoryProId = updateproductVM.CategoryId;
            existingproduct.Price = updateproductVM.Price;
            existingproduct.DiscountPrice = updateproductVM.DiscountPrice;
            existingproduct.UpdatedAt = DateTime.Now;

            // Check if a new image is uploaded
            if (updateproductVM.Photo != null)
            {
                existingproduct.ImgUrl = updateproductVM.Photo.FileName;
            }

            // Save the changes to the database
            _context.SaveChanges();

            // Redirect to the index action after updating the product
            return RedirectToAction("Index");
        }
    }
}
