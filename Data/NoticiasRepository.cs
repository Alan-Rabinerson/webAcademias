using Microsoft.Data.SqlClient; // o Microsoft.Data.SqlClient

namespace WebAcademias.Data 
{
    public class NoticiasRepository
    {
        private readonly string _connectionString;

        // Inyectamos la configuración para leer el appsettings.json automatícamente
        public NoticiasRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PIME_SITES") ?? throw new InvalidOperationException("Connection string 'PIME_SITES' is not configured.");
        }
        // Método para obtener las noticias para la pagina index
        public List<Noticia> ObtenerUltimasNoticias()
        {
            var listaNoticias = new List<Noticia>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT n.not_id, n.not_titulo, n.not_fecha, n.not_subtitulo, n.not_cuerpo, img.img_path, img.img_nombre FROM dbo.ges_noticias AS n LEFT JOIN dbo.ges_imagenes AS img ON n.not_imagen_portada = img.img_id ORDER BY n.not_id DESC";
                
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listaNoticias.Add(new Noticia
                        {
                            Id = reader.GetInt64(0),
                            Titulo = reader.GetString(1),
                            Fecha = reader.GetDateTime(2),
                            Subtitulo = reader.IsDBNull(3) ? null : reader.GetString(3),
                            ImagenRuta = reader.IsDBNull(5) ? null : reader.GetString(5),
                            ImagenNombre = reader.IsDBNull(6) ? null : reader.GetString(6)
                        });
                    }
                }
            }
            return listaNoticias;
        }

        // Método para obtener las noticias para la pagina noticias
        public List<Noticia> ObtenerUltimasNoticiasIndex()
        {
            var listaNoticias = new List<Noticia>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT TOP 4 n.not_id, n.not_titulo, n.not_fecha, n.not_subtitulo, n.not_cuerpo, img.img_path, img.img_nombre, na.noa_asociacion FROM dbo.ges_noticias AS n LEFT JOIN dbo.ges_imagenes AS img ON n.not_imagen_portada = img.img_id LEFT JOIN dbo.ges_noticia_asociacion na ON n.not_id = na.noa_noticia WHERE na.noa_asociacion = 'ACA' ORDER BY n.not_id DESC";
                
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listaNoticias.Add(new Noticia
                        {
                            Id = reader.GetInt64(0),
                            Titulo = reader.GetString(1),
                            Fecha = reader.GetDateTime(2),
                            Subtitulo = reader.IsDBNull(3) ? null : reader.GetString(3),
                            ImagenRuta = reader.IsDBNull(5) ? null : reader.GetString(5),
                            ImagenNombre = reader.IsDBNull(6) ? null : reader.GetString(6)
                        });
                    }
                }
            }
            return listaNoticias;
        }

        public Noticia? ObtenerNoticiaPorId(long id)
        {
            Noticia? noticia = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT n.not_id, n.not_titulo, n.not_fecha, n.not_subtitulo, n.not_cuerpo, img.img_path, img.img_nombre, na.noa_asociacion FROM dbo.ges_noticias AS n LEFT JOIN dbo.ges_imagenes AS img ON n.not_imagen_portada = img.img_id LEFT JOIN dbo.ges_noticia_asociacion na ON n.not_id = na.noa_noticia WHERE n.not_id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            noticia = new Noticia
                            {
                                Id = reader.GetInt64(0),
                                Titulo = reader.GetString(1),
                                Fecha = reader.GetDateTime(2),
                                Subtitulo = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Cuerpo = reader.IsDBNull(4) ? null : reader.GetString(4),
                                ImagenRuta = reader.IsDBNull(5) ? null : reader.GetString(5),
                                ImagenNombre = reader.IsDBNull(6) ? null : reader.GetString(6),
                                Asociacion = reader.IsDBNull(7) ? null : reader.GetString(7)
                            };
                        }
                    }
                }
            }
            return noticia;
        }

        public List<Noticia> BuscarNoticias(string query)
        {
            var listaNoticias = new List<Noticia>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sqlQuery = "SELECT TOP 4 n.not_id, n.not_titulo, n.not_fecha, n.not_subtitulo, img.img_path, img.img_nombre FROM dbo.ges_noticias AS n LEFT JOIN dbo.ges_imagenes AS img ON n.not_imagen_portada = img.img_id LEFT JOIN dbo.ges_noticia_asociacion AS na ON n.not_id = na.noa_noticia WHERE (n.not_titulo LIKE @query OR n.not_subtitulo LIKE @query OR n.not_cuerpo LIKE @query) AND na.noa_asociacion = 'ACA' ORDER BY n.not_id DESC";
                
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@query", $"%{query}%");
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaNoticias.Add(new Noticia
                            {
                                Id = reader.GetInt64(0),
                                Titulo = reader.GetString(1),
                                Fecha = reader.GetDateTime(2),
                                Subtitulo = reader.IsDBNull(3) ? null : reader.GetString(3),
                                ImagenRuta = reader.IsDBNull(4) ? null : reader.GetString(4),
                                ImagenNombre = reader.IsDBNull(5) ? null : reader.GetString(5)
                            });
                        }
                    }
                }
            }
            return listaNoticias;
        }
    }

    

    // Clase auxiliar para mapear los datos (puedes crearla en su propio archivo en una carpeta Models)
    public class Noticia
    {
        public long Id { get; set; }
        public string? Titulo { get; set; }
        public DateTime Fecha { get; set; }
        public string? Subtitulo { get; set; }
        public string? Cuerpo { get; set; }
        public string? Asociacion { get; set; }
        public string? ImagenRuta { get; set; }
        public string? ImagenNombre { get; set; }
    }
}