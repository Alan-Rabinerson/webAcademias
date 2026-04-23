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
        private readonly AcademiasRepository _academiasRepository;
        public Dictionary<string, double> porcentajes;
        public IList<Categoria> categorias;
        public IList<Academia> academias;

        public IndexModel(ILogger<IndexModel> logger, NoticiasRepository noticiasRepository, CategoriasRepository categoriasRepository, AcademiasRepository academiasRepository)
        {
            _logger = logger;
            _noticiasRepository = noticiasRepository;
            _categoriasRepository = categoriasRepository;
            _academiasRepository = academiasRepository;
            categorias = _categoriasRepository.ObtenerTodasCategorias();   
            porcentajes = _categoriasRepository.ObtenerPorcentajesMaterias();       
            academias = _academiasRepository.ObtenerTodasAcademias();
        }
        public List<Noticia> noticias = [];
        public void OnGet()
        {
            noticias = _noticiasRepository.ObtenerUltimasNoticiasIndex();
        }
    }
}
