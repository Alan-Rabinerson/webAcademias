using Microsoft.Data.SqlClient; // o Microsoft.Data.SqlClient

namespace WebAcademias.Data 
{
    public class CategoriasRepository
    {
        private readonly string _connectionString;

        // Inyectamos la configuración para leer el appsettings.json automatícamente
        public CategoriasRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PIME_SITES") ?? throw new InvalidOperationException("Connection string 'PIME_SITES' is not configured.");
        }

        public IList<Categoria> ObtenerTodasCategorias()
        {
            var categorias = new List<Categoria>();
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM dbo.aca_categorias a";

                using SqlCommand command = new(query, connection);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var categoria = new Categoria
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2)
                    };
                    categorias.Add(categoria);
                }
            }
            return categorias;
        }

        public Categoria? ObtenerCategoriaPorId(long id)
        {
            Categoria? categoria = null;
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM dbo.aca_categorias a WHERE a.Id = @Id";

                using SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                using SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    categoria = new Categoria
                    {
                        Id = reader.GetInt64(0),
                        Nombre = reader.GetString(1),
                        Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2)
                    };
                }
            }
            return categoria;
        }

        public long[] ObtenerIdsCategorias()
        {
            var ids = new List<long>();
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();
                string query = "SELECT Id FROM dbo.aca_categorias a";

                using SqlCommand command = new(query, connection);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ids.Add(reader.GetInt64(0));
                }
            }
            return ids.ToArray();
        }

        public Dictionary<string, double> ObtenerPorcentajesMaterias ()
        {
            var porcentajes = new Dictionary<string, double>();
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();
                string query = "SELECT DISTINCT COUNT(*) FROM dbo.aca_academias_categorias WHERE cat_id IN (SELECT cat_id FROM dbo.aca_categorias a WHERE a.cat_materia = @Materia)";
                string totalQuery = "SELECT COUNT(*) as c FROM dbo.aca_academias";
                string[] materias = ["refuerzo escolar", "informatica", "bienestar y salud", "idiomas", "otros"];
                using SqlCommand command = new(query, connection);
                using SqlCommand totalCommand = new(totalQuery, connection);
                using SqlDataReader totalReader = totalCommand.ExecuteReader();
                totalReader.Read();
                var total = totalReader.GetInt32(0);
                totalReader.Close();
                foreach (string materia in materias)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Materia", materia);
                    using SqlDataReader reader = command.ExecuteReader();
                    double porcentaje = reader.Read() ? reader.GetInt32(0) * 100.0 / total :0;
                    porcentajes.Add(materia, porcentaje);
                }
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