using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace WebApp.Controllers
{
    public class SediiController : Controller
    {
        [HttpGet]
        public IActionResult ViewSedii()
        {
            List<Tuple<string, string>> sedii = new List<Tuple<string, string>>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=Curier;User=root;Password=root1234"))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT oras, orar FROM sedii", conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string oras = reader["oras"].ToString();
                        string orar = reader["orar"].ToString();
                        sedii.Add(new Tuple<string, string>(oras, orar));
                    }


                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving sedii: " + ex.Message);
                return View("Error"); // Afișează o pagină de eroare
            }

            return View(sedii); // Returnează lista de sedii către view
        }

    }
}
