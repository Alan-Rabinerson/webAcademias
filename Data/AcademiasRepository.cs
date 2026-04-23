using Microsoft.Data.SqlClient; // o Microsoft.Data.SqlClient

namespace WebAcademias.Data 
{
    public class AcademiasRepository
    {
        private readonly string _connectionString;

        // Inyectamos la configuración para leer el appsettings.json automatícamente
        public AcademiasRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PIME_SITES") ?? throw new InvalidOperationException("Connection string 'PIME_SITES' is not configured.");
        }

        public IList<Academia>? ObtenerTodasAcademias()
        {
            var academias = new List<Academia>();
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();
                string query = "SELECT a.aca_id, a.aca_nombre, a.aca_descripcion, a.aca_poblacion, i.img_path FROM dbo.aca_academias a JOIN dbo.aca_imagenes i ON a.aca_logo = i.img_id";

                using SqlCommand command = new(query, connection);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var academia = new Academia
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2),                        
                        Poblacion = reader.IsDBNull(3) ? null : reader.GetString(3),
                        LogoRuta = reader.IsDBNull(4) ? null : reader.GetString(4),
                    };
                    academias.Add(academia);
                }
            }
            return academias;
        }

        public Academia? ObtenerAcademiaPorId(long id)
        {
            Academia? academia = null;
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();
                string query = "SELECT a.aca_id, a.aca_nombre, a.aca_descripcion, a.aca_poblacion, i.img_path FROM dbo.aca_academias a JOIN dbo.aca_imagenes i ON a.aca_logo = i.img_id WHERE a.aca_id = @id";

                using SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@id", id);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    academia = new Academia
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Poblacion = reader.IsDBNull(3) ? null : reader.GetString(3),
                        Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2),
                        LogoRuta = reader.IsDBNull(4) ? null : reader.GetString(4),
                    };
                }
            }
            return academia;
        }

        public List<Academia> BuscarAcademias(string query)
        {
            var listaAcademias = new List<Academia>();
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();
                string sqlQuery = "SELECT a.aca_id, a.aca_nombre, a.aca_descripcion, a.aca_poblacion, i.img_path FROM dbo.aca_academias a JOIN dbo.aca_imagenes i ON a.aca_logo = i.img_id WHERE a.aca_nombre LIKE @query OR a.aca_descripcion LIKE @query OR a.aca_poblacion LIKE @query";
                
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@query", $"%{query}%");
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaAcademias.Add(new Academia
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Poblacion = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2),
                                LogoRuta = reader.IsDBNull(4) ? null : reader.GetString(4)
                            });
                        }
                    }
                }
            }
            return listaAcademias;
        }

        public List<Academia> BuscarAcademiasPorCategoria(long id)
        {
            var listaAcademias = new List<Academia>();
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();
                    string sqlQuery = "SELECT a.aca_id, a.aca_nombre, a.aca_descripcion, a.aca_poblacion, i.img_path FROM dbo.aca_academias a JOIN dbo.aca_imagenes i ON a.aca_logo = i.img_id WHERE a.aca_id IN (SELECT ac.acat_academia FROM dbo.aca_academia_categoria ac WHERE ac.acat_categoria = @CategoryId)";
                    
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@CategoryId", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listaAcademias.Add(new Academia
                                {
                                    Id = reader.GetInt32(0),
                                    Nombre = reader.GetString(1),
                                    Poblacion = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    LogoRuta = reader.IsDBNull(4) ? null : reader.GetString(4)
                                });
                            }
                        }
                    }
                }
                return listaAcademias;
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
    }
}