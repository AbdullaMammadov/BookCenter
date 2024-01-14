using FinalProject.Models;


namespace FinalProject.Services
{
    public interface ILayoutService
    {
        List<Category> GetAllCategories();
        List<CategoryPro> GetAllCategoryPros();
    }
}