using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ContactVM contactVM = new ContactVM();
     
            return View(contactVM);


        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult SendMessage(Contact contact)
        {
            
                contact.AddedBy = contact.Name;
                contact.CreatedAt = DateTime.Now;
                contact.Body = null;

                // Set a default value for Subject if it's null
                if (contact.Subject == null)
                {
                    contact.Subject = "Default Subject";
                }

                _context.Contacts.Add(contact);
                _context.SaveChanges();
                return RedirectToAction("Index");
            
        }

    }
}
