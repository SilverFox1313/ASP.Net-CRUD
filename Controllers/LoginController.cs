using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using modulo10proyectofinal.Models;

namespace modulo10proyectofinal.Controllers
{
    public class LoginController : Controller
    {
        private readonly string connectionString;

        public LoginController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IActionResult Index()
        {
            List<Login> usuarios = new List<Login>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Login";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    usuarios.Add(new Login
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Usuario = reader["Usuario"].ToString(),
                        Contrasena = reader["Contrasena"].ToString()
                    });
                }
            }

            return View(usuarios);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Login login)
        {
            if (!ModelState.IsValid)
                return View(login);

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Login (Usuario, Contrasena) VALUES (@Usuario, @Contrasena)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Usuario", login.Usuario);
                cmd.Parameters.AddWithValue("@Contrasena", login.Contrasena);
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Login login = new Login();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Login WHERE Id = @Id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    login.Id = Convert.ToInt32(reader["Id"]);
                    login.Usuario = reader["Usuario"].ToString();
                    login.Contrasena = reader["Contrasena"].ToString();
                }
            }

            return View(login);
        }

        [HttpPost]
        public IActionResult Edit(Login login)
        {
            if (!ModelState.IsValid)
                return View(login);

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Login SET Usuario = @Usuario, Contrasena = @Contrasena WHERE Id = @Id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Usuario", login.Usuario);
                cmd.Parameters.AddWithValue("@Contrasena", login.Contrasena);
                cmd.Parameters.AddWithValue("@Id", login.Id);
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Login login = new Login();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Login WHERE Id = @Id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    login.Id = Convert.ToInt32(reader["Id"]);
                    login.Usuario = reader["Usuario"].ToString();
                    login.Contrasena = reader["Contrasena"].ToString();
                }
            }

            return View(login);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Login WHERE Id = @Id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }
    }
}
