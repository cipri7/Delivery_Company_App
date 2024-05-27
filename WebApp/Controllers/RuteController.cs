using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class RuteController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AdaugaRutaViewModel newSediu)
        {
            string oras = newSediu.Oras;
            string orar = newSediu.Orar;

            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=Curier;User=root;Password=root1234"))
            {
                conn.Open();
                MySqlCommand insert = new MySqlCommand("INSERT INTO sedii (oras, orar) VALUES (@oras, @orar);", conn);
                insert.Parameters.AddWithValue("@oras", oras);
                insert.Parameters.AddWithValue("@orar", orar);
                int rowsAffected = insert.ExecuteNonQuery(); // Execute the insert command

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Sediu added successfully!");
                }
                else
                {
                    // Failed to insert the user
                    ModelState.AddModelError(string.Empty, "Failed to insert Sediu.");
                    return View();
                }
            }

            // Redirect to login after successful registration
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.ShowNavigation = false;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(SediuViewModel sediu)
        {
            List<Oras> sedii = new List<Oras>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=Curier;User=root;Password=root1234"))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand("select * from sedii", conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Oras dbSediu = new Oras();
                        dbSediu.id = Convert.ToInt32(reader["id"]);
                        dbSediu.oras = reader["oras"].ToString();
                        dbSediu.orar = reader["orar"].ToString();
                        sedii.Add(dbSediu);
                    }

                    reader.Close();
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Error connecting to the database.");
                return View(sediu);
            }

            if (!string.IsNullOrEmpty(sediu?.Oras) && !string.IsNullOrEmpty(sediu?.Orar))
            {
                bool isValidUser = sedii.Any(u => u.oras == sediu.Oras && u.orar == sediu.Orar);

                if (isValidUser)
                {
                    HttpContext.Session.SetString("Oras", sediu.Oras);
                    return RedirectToAction("Create", "Pachet");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid Oras and Orar.");
            return View(sediu);
        }
    }
}
