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
    }

    public class Categoria
    {
        public long Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; } = string.Empty;
    }
}