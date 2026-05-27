using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAcademias.Data;

namespace WebAcademias.Pages
{
    public class EmpresaModel : PageModel
    {
        private readonly AcademiasRepository _academiasRepository;

        public AcademiaDetalle? academia;

        public EmpresaModel(AcademiasRepository academiasRepository)
        {
            _academiasRepository = academiasRepository;
        }

        public IActionResult OnGet(string nombre)
        {
            academia = _academiasRepository.ObtenerAcademiaDetallesPorNombre(nombre);

            if (academia is null)
            {
                return NotFound();
            }

            ViewData["Title"] = academia.Nombre;
            return Page();
        }
    }
}
