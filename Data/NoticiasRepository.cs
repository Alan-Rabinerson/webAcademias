using Microsoft.Data.SqlClient;
using WebAcademias.Models; // o Microsoft.Data.SqlClient
using Microsoft.EntityFrameworkCore;
namespace WebAcademias.Data
{
    public class NoticiasRepository(AcademiasContext context)
    {
        private readonly AcademiasContext _context = context;

        public List<Noticia> ObtenerUltimasNoticias()
        {
            var query = from n in _context.GesNoticias
                        join img in _context.GesImagenes on (long?)n.NotImagenPortada equals (long?)img.ImgId into imgJoin
                        from img in imgJoin.DefaultIfEmpty()
                        orderby n.NotId descending
                        select new Noticia
                        {
                            Id = n.NotId,
                            Titulo = n.NotTitulo,
                            Fecha = n.NotFecha,
                            Subtitulo = n.NotSubtitulo,
                            Cuerpo = n.NotCuerpo,
                            ImagenRuta = img != null ? img.ImgPath : null,
                            ImagenNombre = img != null ? img.ImgNombre : null
                        };

            return query.ToList();
        }

        // Método para obtener las noticias para la pagina noticias
        public List<Noticia> ObtenerUltimasNoticiasIndex()
        {
            var query = from n in _context.GesNoticias
                        join img in _context.GesImagenes on (long?)n.NotImagenPortada equals (long?)img.ImgId into imgJoin
                        from img in imgJoin.DefaultIfEmpty()
                        join na in _context.GesNoticiaAsociaciones on (long?)n.NotId equals (long?)na.NoaNoticia into naJoin
                        from na in naJoin.DefaultIfEmpty()
                        where na != null && na.NoaAsociacion == "ACA"
                        orderby n.NotId descending
                        select new Noticia
                        {
                            Id = n.NotId,
                            Titulo = n.NotTitulo,
                            Fecha = n.NotFecha,
                            Subtitulo = n.NotSubtitulo,
                            Cuerpo = n.NotCuerpo,
                            ImagenRuta = img != null ? img.ImgPath : null,
                            ImagenNombre = img != null ? img.ImgNombre : null,
                            Asociacion = na != null ? na.NoaAsociacion : null
                        };

            return query.ToList();
        }

        public Noticia? ObtenerNoticiaPorId(long id)
        {
            var query = from n in _context.GesNoticias
                        join img in _context.GesImagenes on (long?)n.NotImagenPortada equals (long?)img.ImgId into imgJoin
                        from img in imgJoin.DefaultIfEmpty()
                        join na in _context.GesNoticiaAsociaciones on (long?)n.NotId equals (long?)na.NoaNoticia into naJoin
                        from na in naJoin.DefaultIfEmpty()
                        where n.NotId == id
                        select new Noticia
                        {
                            Id = n.NotId,
                            Titulo = n.NotTitulo,
                            Fecha = n.NotFecha,
                            Subtitulo = n.NotSubtitulo,
                            Cuerpo = n.NotCuerpo,
                            ImagenRuta = img != null ? img.ImgPath : null,
                            ImagenNombre = img != null ? img.ImgNombre : null,
                            Asociacion = na != null ? na.NoaAsociacion : null
                        };

            return query.FirstOrDefault();
        }

        public List<Noticia> BuscarNoticiasLinq(string query)
        {
            var SQLquery = from n in _context.GesNoticias
                           join img in _context.GesImagenes on (long?)n.NotImagenPortada equals (long?)img.ImgId into imgJoin
                           from img in imgJoin.DefaultIfEmpty()
                           join na in _context.GesNoticiaAsociaciones on (long?)n.NotId equals (long?)na.NoaNoticia into naJoin
                           from na in naJoin.DefaultIfEmpty()
                           where ((n.NotTitulo ?? string.Empty).Contains(query) || (n.NotSubtitulo ?? string.Empty).Contains(query) || (n.NotCuerpo ?? string.Empty).Contains(query))
                           && na != null && na.NoaAsociacion == "ACA"
                           orderby n.NotId descending
                           select new Noticia
                           {
                               Id = n.NotId,
                               Titulo = n.NotTitulo,
                               Fecha = n.NotFecha,
                               Subtitulo = n.NotSubtitulo,
                               Cuerpo = n.NotCuerpo,
                               ImagenRuta = img != null ? img.ImgPath : null,
                               ImagenNombre = img != null ? img.ImgNombre : null,
                               Asociacion = na != null ? na.NoaAsociacion : null
                           };

            return SQLquery.Take(4).ToList();
        }

        public List<Noticia> BuscarNoticias(string query)
        {
            var SQLquery = from n in _context.GesNoticias
                           join img in _context.GesImagenes on (long?)n.NotImagenPortada equals (long?)img.ImgId into imgJoin
                           from img in imgJoin.DefaultIfEmpty()
                           join na in _context.GesNoticiaAsociaciones on (long?)n.NotId equals (long?)na.NoaNoticia into naJoin
                           from na in naJoin.DefaultIfEmpty()
                           where ((n.NotTitulo ?? string.Empty).Contains(query) || (n.NotSubtitulo ?? string.Empty).Contains(query) || (n.NotCuerpo ?? string.Empty).Contains(query))
                           && na != null && na.NoaAsociacion == "ACA"
                           orderby n.NotId descending
                           select new Noticia
                           {
                               Id = n.NotId,
                               Titulo = n.NotTitulo,
                               Fecha = n.NotFecha,
                               Subtitulo = n.NotSubtitulo,
                               Cuerpo = n.NotCuerpo,
                               ImagenRuta = img != null ? img.ImgPath : null,
                               ImagenNombre = img != null ? img.ImgNombre : null,
                               Asociacion = na != null ? na.NoaAsociacion : null
                           };

            return [.. SQLquery.Take(4)];
        }
    }
}