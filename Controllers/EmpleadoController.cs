using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using System.Data;
using modulo10proyectofinal.Models;

namespace modulo10proyectofinal.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly string connectionString;

        public EmpleadoController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IActionResult Index()
        {
            List<Empleado> empleados = new List<Empleado>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
            SELECT e.Id, e.Nombre, e.Apellido, e.Email, e.Telefono, e.PaisId, p.Nombre AS PaisNombre
            FROM Empleado e
            INNER JOIN Pais p ON e.PaisId = p.Id";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    empleados.Add(new Empleado
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nombre = reader["Nombre"].ToString(),
                        Apellido = reader["Apellido"].ToString(),
                        Email = reader["Email"].ToString(),
                        Telefono = reader["Telefono"].ToString(),
                        PaisId = Convert.ToInt32(reader["PaisId"]),
                        PaisNombre = reader["PaisNombre"].ToString()
                    });
                }
            }

            return View(empleados);
        }


        // GET: Empleado/Create
        public IActionResult Create()
        {
            ViewBag.Paises = ObtenerListaPaises();
            return View();
        }

        // POST: Empleado/Create
        [HttpPost]
        public IActionResult Create(Empleado emp)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO Empleado (Nombre, Apellido, Email, Telefono, PaisId)
                                 VALUES (@Nombre, @Apellido, @Email, @Telefono, @PaisId)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nombre", emp.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", emp.Apellido);
                cmd.Parameters.AddWithValue("@Email", emp.Email);
                cmd.Parameters.AddWithValue("@Telefono", emp.Telefono);
                cmd.Parameters.AddWithValue("@PaisId", emp.PaisId);
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // GET: Empleado/Edit/5
        public IActionResult Edit(int id)
        {
            Empleado emp = new Empleado();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Empleado WHERE Id = @Id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    emp.Id = Convert.ToInt32(reader["Id"]);
                    emp.Nombre = reader["Nombre"].ToString();
                    emp.Apellido = reader["Apellido"].ToString();
                    emp.Email = reader["Email"].ToString();
                    emp.Telefono = reader["Telefono"].ToString();
                    emp.PaisId = Convert.ToInt32(reader["PaisId"]);
                }
            }

            ViewBag.Paises = ObtenerListaPaises();
            return View(emp);
        }

        // POST: Empleado/Edit
        [HttpPost]
        public IActionResult Edit(Empleado emp)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"UPDATE Empleado SET 
                                 Nombre = @Nombre, 
                                 Apellido = @Apellido, 
                                 Email = @Email, 
                                 Telefono = @Telefono, 
                                 PaisId = @PaisId 
                                 WHERE Id = @Id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nombre", emp.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", emp.Apellido);
                cmd.Parameters.AddWithValue("@Email", emp.Email);
                cmd.Parameters.AddWithValue("@Telefono", emp.Telefono);
                cmd.Parameters.AddWithValue("@PaisId", emp.PaisId);
                cmd.Parameters.AddWithValue("@Id", emp.Id);
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // GET: Empleado/Delete/5
        public IActionResult Delete(int id)
        {
            Empleado emp = new Empleado();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Empleado WHERE Id = @Id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    emp.Id = Convert.ToInt32(reader["Id"]);
                    emp.Nombre = reader["Nombre"].ToString();
                    emp.Apellido = reader["Apellido"].ToString();
                    emp.Email = reader["Email"].ToString();
                    emp.Telefono = reader["Telefono"].ToString();
                    emp.PaisId = Convert.ToInt32(reader["PaisId"]);
                }
            }

            return View(emp);
        }

        // POST: Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Empleado WHERE Id = @Id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // 🔽 Método privado para cargar países
        private List<SelectListItem> ObtenerListaPaises()
        {
            var lista = new List<SelectListItem>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Id, Nombre FROM Pais";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new SelectListItem
                    {
                        Value = reader["Id"].ToString(),
                        Text = reader["Nombre"].ToString()
                    });
                }
            }

            return lista;
        }
    }
}
