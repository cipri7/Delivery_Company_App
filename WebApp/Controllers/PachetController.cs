using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using WebApp;
    public class PachetController : Controller
    {
        // GET: Pachet/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumeExpeditor,TelefonExpeditor,OrasPlecare,OrasDestinatie,NumeDestinatar,TelefonDestinatar,Greutate,CategorieSpeciala")] Pachet pachet)
        {
            if (ModelState.IsValid)
            {
                string connectionString = "Server=localhost;Database=Curier;User=root;Password=root1234";

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    MySqlCommand insert = new MySqlCommand(
                        "INSERT INTO pachete (numeExpeditor, telefonExpeditor, orasPlecare, orasDestinatie, numeDestinatar, telefonDestinatar, greutate, categorieSpeciala) " +
                        "VALUES (@numeExpeditor, @telefonExpeditor, @orasPlecare, @orasDestinatie, @numeDestinatar, @telefonDestinatar, @greutate, @categorieSpeciala);", conn);

                    insert.Parameters.AddWithValue("@numeExpeditor", pachet.NumeExpeditor);
                    insert.Parameters.AddWithValue("@telefonExpeditor", pachet.TelefonExpeditor);
                    insert.Parameters.AddWithValue("@orasPlecare", pachet.OrasPlecare);
                    insert.Parameters.AddWithValue("@orasDestinatie", pachet.OrasDestinatie);
                    insert.Parameters.AddWithValue("@numeDestinatar", pachet.NumeDestinatar);
                    insert.Parameters.AddWithValue("@telefonDestinatar", pachet.TelefonDestinatar);
                    insert.Parameters.AddWithValue("@greutate", pachet.Greutate);
                    insert.Parameters.AddWithValue("@categorieSpeciala", pachet.CategorieSpeciala);

                    int rowsAffected = await insert.ExecuteNonQueryAsync();

                    if (rowsAffected > 0)
                    {
                        double costLivrare = pachet.CalculeazaCost();
                        return RedirectToAction("Success", new { cost = costLivrare });
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Failed to insert Pachet.");
                    }
                }
            }
            return View(pachet);
        }


        // GET: Pachet/Success
        public IActionResult Success(double cost)
        {
            ViewBag.CostLivrare = cost;
            return View();
        }
        
        [HttpPost]
        public IActionResult Accept(double cost)
        {
            // Aici poți adăuga logica pentru a procesa acceptarea expediției

            ViewBag.AcceptedCost = cost;
            return View("Accepted");
        }
        
        // GET: Pachet/ViewAll
        public IActionResult ViewAll()
        {
            List<Pachet> pachete = new List<Pachet>();

            try
            {
                string connectionString = "Server=localhost;Database=Curier;User=root;Password=root1234";

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM pachete", conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Pachet pachet = new Pachet
                        {
                            NumeExpeditor = reader["numeExpeditor"].ToString(),
                            TelefonExpeditor = reader["telefonExpeditor"].ToString(),
                            OrasPlecare = reader["orasPlecare"].ToString(),
                            OrasDestinatie = reader["orasDestinatie"].ToString(),
                            NumeDestinatar = reader["numeDestinatar"].ToString(),
                            TelefonDestinatar = reader["telefonDestinatar"].ToString(),
                            Greutate = Convert.ToDouble(reader["greutate"]),
                            CategorieSpeciala = reader["categorieSpeciala"].ToString()
                        };
                        pachete.Add(pachet);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving pachete: " + ex.Message);
                return View("Error"); // Afișează o pagină de eroare
            }

            return View(pachete); // Returnează lista de pachete către view
        }

    }