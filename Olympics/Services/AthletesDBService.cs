using Olympics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Olympics.Services
{
    public class AthletesDBService
    {
        private readonly SqlConnection _connection;

        public AthletesDBService(SqlConnection connection)
        {
            _connection = connection;
        }

        public List<AthleteModel> GetListOfAthletes()
        {
            List<AthleteModel> athletes = new();

            _connection.Open();

            using var command = new SqlCommand("SELECT * FROM dbo.Atletes;", _connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                AthleteModel athlete = new()
                {
                    ID = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Surname = reader.GetString(2),
                    Country = reader.GetString(3)
                };

                athletes.Add(athlete);
            }

            _connection.Close();

            return athletes;
        }
    }
}
