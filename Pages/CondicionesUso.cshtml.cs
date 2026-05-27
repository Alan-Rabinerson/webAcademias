using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAcademias.Pages
{
    public class CondicionesUsoModel : PageModel
    {
        private readonly ILogger<CondicionesUsoModel> _logger;

        public CondicionesUsoModel(ILogger<CondicionesUsoModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
