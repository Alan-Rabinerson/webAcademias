using Microsoft.Data.SqlClient; // o Microsoft.Data.SqlClient

namespace TuProyecto.Repositories 
{
    public class AcademiasRepository
    {
        private readonly string _connectionString;

        // Inyectamos la configuración para leer el appsettings.json automatícamente
        public AcademiasRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PIME_SITES") ?? throw new InvalidOperationException("Connection string 'PIME_SITES' is not configured.");
        }

        public Academia? ObtenerTodasAcademias()
        {
            Academia? academia = null;
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();
                string query = "SELECT a.aca_id, a.aca_nombre, a.aca_descripcion, a.aca_poblacion, a.aca_logo FROM dbo.aca_academias a";

                using SqlCommand command = new(query, connection);
                using SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    academia = new Academia
                    {
                        Id = reader.GetInt64(0),
                        Nombre = reader.GetString(1),
                        Poblacion = reader.IsDBNull(2) ? null : reader.GetString(2),
                        Descripcion = reader.IsDBNull(3) ? null : reader.GetString(3),
                        LogoRuta = reader.IsDBNull(4) ? null : reader.GetString(4),
                        Materia = reader.IsDBNull(5) ? null : reader.GetString(5)
                    };
                }
            }
            return academia;
        }

        public Academia? ObtenerAcademiaPorId(long id)
        {
            Academia? academia = null;
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();
                string query = "SELECT a.aca_id, a.aca_nombre, a.aca_descripcion, a.aca_poblacion, a.aca_logo FROM dbo.aca_academias a WHERE a.aca_id = @id";

                using SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@id", id);
                using SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    academia = new Academia
                    {
                        Id = reader.GetInt64(0),
                        Nombre = reader.GetString(1),
                        Poblacion = reader.IsDBNull(2) ? null : reader.GetString(2),
                        Descripcion = reader.IsDBNull(3) ? null : reader.GetString(3),
                        LogoRuta = reader.IsDBNull(4) ? null : reader.GetString(4),
                        Materia = reader.IsDBNull(5) ? null : reader.GetString(5)

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
                string sqlQuery = "SELECT a.aca_id, a.aca_nombre, a.aca_descripcion, a.aca_poblacion, a.aca_logo FROM dbo.aca_academias a WHERE a.aca_nombre LIKE @query OR a.aca_descripcion LIKE @query OR a.aca_poblacion LIKE @query";
                
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@query", $"%{query}%");
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaAcademias.Add(new Academia
                            {
                                Id = reader.GetInt64(0),
                                Nombre = reader.GetString(1),
                                Poblacion = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Descripcion = reader.IsDBNull(3) ? null : reader.GetString(3),
                                LogoRuta = reader.IsDBNull(4) ? null : reader.GetString(4)
                            });
                        }
                    }
                }
            }
            return listaAcademias;
        }

        public List<Academia> BuscarAcademiasPorCategoria(string query)
            {
                var listaAcademias = new List<Academia>();
                using (SqlConnection connection = new(_connectionString))
                {
                    connection.Open();
                    string sqlQuery = "SELECT a.aca_id, a.aca_nombre, a.aca_descripcion, a.aca_poblacion, a.aca_logo FROM dbo.aca_academias a WHERE @query IN (c.cat_nombre from dbo.aca_categorias c LEFT JOIN dbo.aca_academia_categoria ac ON c.cat_id = ac.acat_categoria WHERE ac.acat_academia = a.aca_id)";
                    
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@query", $"%{query}%");
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listaAcademias.Add(new Academia
                                {
                                    Id = reader.GetInt64(0),
                                    Nombre = reader.GetString(1),
                                    Poblacion = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    Descripcion = reader.IsDBNull(3) ? null : reader.GetString(3),
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