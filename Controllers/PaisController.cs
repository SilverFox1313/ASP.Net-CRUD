using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using modulo10proyectofinal.Models;

namespace modulo10proyectofinal.Controllers
{
    public class PaisController : Controller
    {
        private readonly string connectionString;

        public PaisController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // GET: /Pais
        public IActionResult Index()
        {
            List<Pais> paises = new List<Pais>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Pais";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    paises.Add(new Pais
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nombre = reader["Nombre"].ToString()
                    });
                }
            }

            return View(paises);
        }

        // GET: /Pais/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Pais/Create
        [HttpPost]
        public IActionResult Create(Pais pais)
        {
            if (!ModelState.IsValid)
                return View(pais);

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Pais (Nombre) VALUES (@Nombre)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nombre", pais.Nombre);
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // GET: /Pais/Edit/5
        public IActionResult Edit(int id)
        {
            Pais pais = new Pais();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Pais WHERE Id = @Id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    pais.Id = Convert.ToInt32(reader["Id"]);
                    pais.Nombre = reader["Nombre"].ToString();
                }
            }

            return View(pais);
        }

        // POST: /Pais/Edit
        [HttpPost]
        public IActionResult Edit(Pais pais)
        {
            if (!ModelState.IsValid)
                return View(pais);

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Pais SET Nombre = @Nombre WHERE Id = @Id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nombre", pais.Nombre);
                cmd.Parameters.AddWithValue("@Id", pais.Id);
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // GET: /Pais/Delete/5
        public IActionResult Delete(int id)
        {
            Pais pais = new Pais();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Pais WHERE Id = @Id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    pais.Id = Convert.ToInt32(reader["Id"]);
                    pais.Nombre = reader["Nombre"].ToString();
                }
            }

            return View(pais);
        }

        // POST: /Pais/Delete
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Pais WHERE Id = @Id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }
    }
}
