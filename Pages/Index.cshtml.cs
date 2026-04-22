using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAcademias.Data;

namespace WebAcademias.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly NoticiasRepository _noticiasRepository;
        private readonly CategoriasRepository _categoriasRepository; 
        public Dictionary<string, double> porcentajes;
        public IList<Categoria> categorias;
        public IndexModel(ILogger<IndexModel> logger, NoticiasRepository noticiasRepository, CategoriasRepository categoriasRepository)
        {
            _logger = logger;
            _noticiasRepository = noticiasRepository;
            _categoriasRepository = categoriasRepository;
            categorias = _categoriasRepository.ObtenerTodasCategorias();   
            porcentajes = _categoriasRepository.ObtenerPorcentajesMaterias();             
        }
        public List<Noticia> noticias = [];
        public void OnGet()
        {
            noticias = _noticiasRepository.ObtenerUltimasNoticias();
        }
    }
}
