using FinalProject.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjec.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class SaleController : Controller
    {
        private readonly AppDbContext _context;

        public SaleController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var sale = _context.Sales
                .Include(s => s.AppUser)
                .Include(s => s.SaleProducts)
                .ThenInclude(s => s.Product)
                .Include(s=>s.SaleBooks)
                .ThenInclude(s=>s.Book)
                .ToList();
            return View(sale);
        }
    }
}