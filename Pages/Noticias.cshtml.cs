using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAcademias.Data;

namespace WebAcademias.Pages
{
    public class NoticiasModel : PageModel
    {
        private readonly NoticiasRepository _noticiasRepository;
        public List<Noticia> noticias = new();
        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }
        public NoticiasModel(NoticiasRepository noticiasRepository)
        {
            _noticiasRepository = noticiasRepository;
        }
        public void OnGet()
        {
            if (!string.IsNullOrWhiteSpace(Search))
            {
                noticias = _noticiasRepository.BuscarNoticias(Search);
            }
            else
            {
                noticias = _noticiasRepository.ObtenerUltimasNoticias();
            }
        }
    }
}
