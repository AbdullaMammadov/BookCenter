using FinalProject.DAL;
using FinalProject.Helpers;
using FinalProject.Models;
using FinalProject.ViewModel;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;

        public ShopController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Books(int page = 1, int take = 18)
        {
            ShopVM shopVM = new();

            shopVM.Products = _context.Products
                .Where(x => x.DiscountPrice != null)
                 .OrderByDescending(s => s.Id)
                .Take(6)
                .ToList();
            shopVM.Books = _context.Books
                 .OrderByDescending(s => s.Id)
                .Skip((page - 1) * take)
                .Take(take)
                .ToList();
            var count = _context.Books.Count();
            int pageCount = (int)Math.Ceiling((decimal)count / take);

            Pagination<Book> paginationB = new(shopVM.Books, pageCount, page,shopVM.Products,null);

            return View(paginationB);

        }

        public IActionResult Products(int page = 1, int take = 18)
        {
            ShopVM shopVM = new();

            shopVM.Books = _context.Books
                .Where(x => x.DiscountPrice != null)
                 .OrderByDescending(s => s.Id)
                .Take(6)
                .ToList();
            shopVM.Products = _context.Products
                 .OrderByDescending(s => s.Id)
                .Skip((page - 1) * take)
                .Take(take)
                .ToList();
            var count = _context.Products.Count();
            int pageCount = (int)Math.Ceiling((decimal)count / take);

            Pagination<Product> paginationP = new(shopVM.Products, pageCount, page,  null, shopVM.Books);

            return View(paginationP);

        }






    }


}