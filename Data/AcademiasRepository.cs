using Microsoft.EntityFrameworkCore;
using WebAcademias.Models; // o Microsoft.Data.SqlClient

namespace WebAcademias.Data
{
    public class AcademiasRepository(AcademiasContext context)
    {
        private readonly AcademiasContext _context = context;

        public IList<Academia> ObtenerTodasAcademias()
        {
            var query = from aca in _context.AcaAcademias
                        join imagenes in _context.AcaImagenes on aca.AcaLogo equals imagenes.ImgPath into imgGroup
                        from img in imgGroup.DefaultIfEmpty()
                        select new Academia
                        {
                            Id = aca.AcaId,
                            Nombre = aca.AcaNombre,
                            Descripcion = aca.AcaDescripcion,
                            Poblacion = aca.AcaPoblacion,
                            LogoRuta = img != null ? img.ImgPath : aca.AcaLogo
                        };

            return [.. query];
        }

        public async Task<Academia?> ObtenerAcademiaPorIdAsync(long id)
        {
            Academia? academia = null;
            var aca = await _context.AcaAcademias.FindAsync(id);
            if (aca != null)
            {
                var query = from academias in _context.AcaAcademias
                            where academias.AcaId == id
                            join imagenes in _context.AcaImagenes on academias.AcaLogo equals imagenes.ImgPath into imgGroup
                            from img in imgGroup.DefaultIfEmpty()
                            select new Academia
                            {
                                Id = academias.AcaId,
                                Nombre = academias.AcaNombre,
                                Descripcion = academias.AcaDescripcion,
                                Poblacion = academias.AcaPoblacion,
                                LogoRuta = img != null ? img.ImgPath : academias.AcaLogo
                            };
                academia = await query.FirstOrDefaultAsync();
            }
            return academia;
        }

        public Academia? ObtenerAcademiaPorId(long id)
        {
            var query = from aca in _context.AcaAcademias
                        where aca.AcaId == id
                        join imagenes in _context.AcaImagenes on aca.AcaLogo equals imagenes.ImgPath into imgGroup
                        from img in imgGroup.DefaultIfEmpty()
                        select new Academia
                        {
                            Id = aca.AcaId,
                            Nombre = aca.AcaNombre,
                            Descripcion = aca.AcaDescripcion,
                            Poblacion = aca.AcaPoblacion,
                            LogoRuta = img != null ? img.ImgPath : aca.AcaLogo
                        };
            return query.FirstOrDefault();
        }

        public List<Academia> BuscarAcademias(string query)
        {
            var sqlQuery = from aca in _context.AcaAcademias
                           join imagenes in _context.AcaImagenes on aca.AcaLogo equals imagenes.ImgPath into imgGroup
                           from img in imgGroup.DefaultIfEmpty()
                           where aca.AcaNombre.Contains(query) || (aca.AcaDescripcion != null && aca.AcaDescripcion.Contains(query)) || (aca.AcaPoblacion != null && aca.AcaPoblacion.Contains(query))
                           select new Academia
                           {
                               Id = aca.AcaId,
                               Nombre = aca.AcaNombre,
                               Descripcion = aca.AcaDescripcion,
                               Poblacion = aca.AcaPoblacion,
                               LogoRuta = img != null ? img.ImgPath : aca.AcaLogo
                           };
            return sqlQuery.ToList();
        }

        public List<Academia> BuscarAcademiasPorCategoria(long id)
        {
            var query = from aca in _context.AcaAcademias
                        where aca.Cats.Any(c => c.CatId == (int)id)
                        join imagenes in _context.AcaImagenes on aca.AcaLogo equals imagenes.ImgPath into imgGroup
                        from img in imgGroup.DefaultIfEmpty()
                        select new Academia
                        {
                            Id = aca.AcaId,
                            Nombre = aca.AcaNombre,
                            Descripcion = aca.AcaDescripcion,
                            Poblacion = aca.AcaPoblacion,
                            LogoRuta = img != null ? img.ImgPath : aca.AcaLogo
                        };
            return query.ToList();
        }
}


// Clase auxiliar para mapear los datos (puedes crearla en su propio archivo en una carpeta Models)
public class Academia
{
    public long Id { get; set; }
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public string? LogoRuta { get; set; }
    public string? Poblacion { get; set; }
    public string? Materia { get; set; }

    public static implicit operator Academia?(AcaAcademia? v)
    {
        throw new NotImplementedException();
    }
}
}