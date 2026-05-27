using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAcademias.Data;
using WebAcademias.Models;

namespace WebAcademias.Pages
{
    public class EmpresasModel : PageModel
    {
        private readonly AcademiasRepository _academiasRepository;
        private readonly CategoriasRepository _categoriasRepository;

        public List<Academia> academias { get; set; } = [];
        public IList<Categoria> categorias { get; set; } = [];

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public string? FiltroActivo { get; set; }

        public EmpresasModel(AcademiasRepository academiasRepository, CategoriasRepository categoriasRepository)
        {
            _academiasRepository = academiasRepository;
            _categoriasRepository = categoriasRepository;
        }

        public void OnGet(string? filtro)
        {
            categorias = _categoriasRepository.ObtenerTodasCategorias();
            FiltroActivo = filtro;

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                var categoria = categorias.FirstOrDefault(c =>
                    c.Nombre.ToLower().Replace(" ", "-") == filtro.ToLower());

                academias = categoria != null
                    ? _academiasRepository.BuscarAcademiasPorCategoria(categoria.Id)
                    : [.. _academiasRepository.ObtenerTodasAcademias()];
            }
            else if (!string.IsNullOrWhiteSpace(Search))
            {
                academias = _academiasRepository.BuscarAcademias(Search);
            }
            else
            {
                academias = [.. _academiasRepository.ObtenerTodasAcademias()];
            }
        }
    }
}
