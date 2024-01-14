using FinalProject.Models;

namespace FinalProject.ViewModel
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Activity> Tedbirs { get; set; }
        public List<Activity> Lahiyes { get; set; }
        public List<Category> Categories { get; set; }
        public List<Book> Books { get; set; }
        public List<Book> LastBooks { get; set; }
    }
}
