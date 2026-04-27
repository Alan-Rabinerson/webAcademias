using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAcademias.Data;
using WebAcademias.Models;
using Microsoft.EntityFrameworkCore;
namespace WebAcademias.Pages
{
    public class NoticiaModel : PageModel
    {
        private readonly NoticiasRepository _noticiasRepository;
        public NoticiaModel(NoticiasRepository noticiasRepository)
        {
            _noticiasRepository = noticiasRepository;
        }
        public Noticia? noticia;

        public IActionResult OnGet(long id)
        {
            noticia = _noticiasRepository.ObtenerNoticiaPorId(id); 

            if (noticia is null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
