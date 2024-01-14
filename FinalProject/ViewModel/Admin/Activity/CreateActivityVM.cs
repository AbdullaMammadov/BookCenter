namespace FinalProject.ViewModel.Admin.Activity
{
    public class CreateActivityVM
    {
        public string   Title { get; set; }
        public IFormFile Photo { get; set; }
        public string Description { get; set; }
        public string? Desc2 {  get; set; }
        public string? Desc3 { get; set; }
        public string AddedBy { get; set; }
        public bool? IsTedbir { get; set; }
    }
}
