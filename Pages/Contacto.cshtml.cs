using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAcademias.Pages
{
    public class ContactoModel : PageModel
    {
        private readonly ILogger<ContactoModel> _logger;

        public ContactoModel(ILogger<ContactoModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }

}