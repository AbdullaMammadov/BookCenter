
using System.Reflection;

namespace FinalProject.Models
{
    public class Activity:BaseEntity
    {
       public bool? IsTedbir { get; set; }
        public string ImgUrl { get; set; }
        public string Title {  get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public string? Desc2 { get; set; }
        public string? Desc3 { get; set; }

    }
}
