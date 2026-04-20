using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAcademias.Data;

namespace WebAcademias.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly NoticiasRepository _noticiasRepository;
        private readonly CategoriasRepository _categoriasRepository; // AÑADIR LA REFERENCIA AL REPOSITORIO DE CATEGORIAS
        public IList<Categoria> categorias;
        public IndexModel(ILogger<IndexModel> logger, NoticiasRepository noticiasRepository, CategoriasRepository categoriasRepository)
        {
            _logger = logger;
            _noticiasRepository = noticiasRepository;
            _categoriasRepository = categoriasRepository;
            categorias = _categoriasRepository.ObtenerTodasCategorias();
            
        }
        public List<Noticia> noticias = new List<Noticia>();
        public void OnGet()
        {
            noticias = _noticiasRepository.ObtenerUltimasNoticias();
        }
    }
}
