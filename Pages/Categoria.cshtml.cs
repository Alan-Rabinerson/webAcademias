using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAcademias.Data;

namespace WebAcademias.Pages
{
    public class CategoriaModel : PageModel
    {
        private readonly CategoriasRepository _categoriasRepository;
        private readonly AcademiasRepository _academiasRepository;
        public CategoriaModel(AcademiasRepository academiasRepository, CategoriasRepository categoriasRepository)
        {
            _categoriasRepository = categoriasRepository;
            _academiasRepository = academiasRepository;
        }
        public Categoria? categoria;

        public List<Academia> academias = new List<Academia>();

        public IActionResult OnGet(long id)
        {
            categoria = _categoriasRepository.ObtenerCategoriaPorId(id);

            academias = _academiasRepository.BuscarAcademiasPorCategoria(id);

            if (categoria is null )
            {
                return NotFound();
            }

            return Page();
        }
    }
}
