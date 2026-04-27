using Microsoft.EntityFrameworkCore;
namespace WebAcademias.Data
{
    public class CategoriasRepository(AcademiasContext context)
    {
        private readonly AcademiasContext _context = context;

        public IList<Categoria> ObtenerTodasCategorias()
        {
            return [.. _context.AcaCategorias
                .Select(c => new Categoria
                {
                    Id = c.CatId,              // ajusta cast si CatId no es long
                    Nombre = c.CatNombre,      // ajusta al nombre real de columna
                    Descripcion = c.CatDescripcion
                })];
        }

        public Categoria? ObtenerCategoriaPorId(long id)
        {
            return _context.AcaCategorias
                .Where(c => c.CatId == (int)id)   // ajusta tipo si CatId no es int
                .Select(c => new Categoria
                {
                    Id = c.CatId,
                    Nombre = c.CatNombre,
                    Descripcion = c.CatDescripcion
                })
                .FirstOrDefault();
        }

        public Dictionary<string, double> ObtenerPorcentajesMaterias()
        {
            var porcentajes = new Dictionary<string, double>();

            string[] materias = ["refuerzo escolar", "informatica", "bienestar y salud", "idiomas", "otros"];
            int totalAcademias = _context.AcaAcademias.Count();

            var conteosPorMateria = _context.AcaCategorias
                .Where(c => c.CatMateria != null)
                .SelectMany(c => c.Acas.Select(_ => c.CatMateria!.ToLower()))
                .GroupBy(m => m)
                .Select(g => new { Materia = g.Key, Conteo = g.Count() })
                .ToDictionary(x => x.Materia, x => x.Conteo);

            foreach (string materia in materias)
            {
                conteosPorMateria.TryGetValue(materia.ToLower(), out int conteo);
                double porcentaje = totalAcademias > 0 ? conteo * 100.0 / totalAcademias : 0;
                porcentajes.Add(materia, porcentaje);
            }

            return porcentajes;
        }
    }

    public class Categoria
    {
        public long Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; } = string.Empty;
    }
}