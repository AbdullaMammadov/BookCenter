using FinalProject.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace FinalProject.ViewModel
{
    public class ActivityVM
    {
       public Activity Tedbir { get; set; }
        public List<Activity> Tedbirs { get; set; }
        public Activity Lahiye { get; set; }
        public List<Activity> Lahiyes { get; set; }
        public List<Book>? books { get; set; }
    }
}
