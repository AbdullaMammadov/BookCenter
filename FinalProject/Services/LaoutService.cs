using FinalProject.DAL;
using FinalProject.Models;

namespace FinalProject.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly AppDbContext _appDbContext;

        public LayoutService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<Category> GetAllCategories()
        {
            var categories= _appDbContext.Categories
            .Where(c => c.Books.Any())
             .ToList();
            return categories;



        }

        public List<CategoryPro> GetAllCategoryPros()
        {
           
          var categories = _appDbContext.CategoryPros
          .Where(c => c.Products.Any()) 
           .ToList();
            return categories;

        }
    }
}