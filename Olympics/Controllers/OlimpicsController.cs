using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Olympics.Models;

namespace Olympics.Controllers
{
    public class OlipicsController : Controller
    {
        //private SqlConnection _connection;
        private readonly OlipicsController(AtletesDBService dBService)

        public OlipicsController(SqlConnection connection)
        {
            _connection = connection;
        }

        public IActionResult Index()
        {
            List<AthleteModel> atletic = new();

            _connection.Open();

            using var command = new SqlCommand("SELECT Id, Name, Surname, Country FROM dbo.AthleticsTable;", _connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                atletic.Add(new AthleteModel()
                {
                    ID = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Surname = reader.GetString(2),
                    Country = reader.GetString(3)
                });
            }

            _connection.Close();

            return View(atletic);
        }

        public IActionResult DisplayAddNewItem()
        {
            return View("AddNewAthletic");
        }

        public IActionResult AddNewAtletic(AthleteModel model)
        {
            _connection.Open();

            string insertText = $"insert into dbo.AtleticsTable (Name, Surname, Country) values('{model.Name}','{model.Surname}', '{model.Country}',); ";

            SqlCommand command = new SqlCommand(insertText, _connection);
            command.ExecuteNonQuery();

            _connection.Close();

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}