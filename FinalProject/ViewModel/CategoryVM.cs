using FinalProject.Models;

namespace FinalProject.ViewModel
{
    public class CategoryVM
    {
      public Category? Category { get; set; }
        public CategoryPro? CategoryPro { get; set; }
        public List<Activity> Activities { get; set; }
    }
}
