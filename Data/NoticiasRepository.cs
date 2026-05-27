using Microsoft.EntityFrameworkCore;
using WebAcademias.Models;
namespace WebAcademias.Data
{
    public class NoticiasRepository(AcademiasContext context)
    {
        private readonly AcademiasContext _context = context;

        public List<GesNoticia> ObtenerUltimasNoticias()
        {
            return _context.GesNoticias
                .Include(n => n.NotImagenPortadaNavigation)
                .Where(n => n.GesNoticiaAsociaciones.Any(na => na.NoaAsociacion == "ACA"))
                .OrderByDescending(n => n.NotId)
                .ToList();
        }

        public List<GesNoticia> ObtenerUltimasNoticiasIndex()
        {
            return _context.GesNoticias
                .Include(n => n.NotImagenPortadaNavigation)
                .Where(n => n.GesNoticiaAsociaciones.Any(na => na.NoaAsociacion == "ACA"))
                .OrderByDescending(n => n.NotId)
                .ToList();
        }

        public GesNoticia? ObtenerNoticiaPorId(long id)
        {
            return _context.GesNoticias
                .Include(n => n.NotImagenPortadaNavigation)
                .FirstOrDefault(n => n.NotId == id);
        }

        public List<GesNoticia> BuscarNoticiasLinq(string query)
        {
            return _context.GesNoticias
                .Include(n => n.NotImagenPortadaNavigation)
                .Where(n =>
                    n.GesNoticiaAsociaciones.Any(na => na.NoaAsociacion == "ACA") &&
                    ((n.NotTitulo ?? string.Empty).Contains(query) ||
                     (n.NotSubtitulo ?? string.Empty).Contains(query) ||
                     (n.NotCuerpo ?? string.Empty).Contains(query)))
                .OrderByDescending(n => n.NotId)
                .Take(4)
                .ToList();
        }

        public List<GesNoticia> BuscarNoticias(string query)
        {
            return _context.GesNoticias
                .Include(n => n.NotImagenPortadaNavigation)
                .Where(n =>
                    n.GesNoticiaAsociaciones.Any(na => na.NoaAsociacion == "ACA") &&
                    ((n.NotTitulo ?? string.Empty).Contains(query) ||
                     (n.NotSubtitulo ?? string.Empty).Contains(query) ||
                     (n.NotCuerpo ?? string.Empty).Contains(query)))
                .OrderByDescending(n => n.NotId)
                .Take(4)
                .ToList();
        }
    }
}
