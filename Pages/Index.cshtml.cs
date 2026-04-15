using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TuProyecto.Repositories;

namespace WebAcademias.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly NoticiasRepository _noticiasRepository;

        public IndexModel(ILogger<IndexModel> logger, NoticiasRepository noticiasRepository)
        {
            _logger = logger;
            _noticiasRepository = noticiasRepository;
        }
        public List<Noticia> noticias = new List<Noticia>();
        public void OnGet()
        {
            noticias = _noticiasRepository.ObtenerUltimasNoticias();
        }
    }
}
