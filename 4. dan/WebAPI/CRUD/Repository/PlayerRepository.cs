using CRUD.DataClasses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CRUD.Repository
{
    public class PlayerRepository
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;

        [HttpPost]
        public static void AddToDB([FromBody] Player player)
        {
            SqlConnection connection = new SqlConnection();
            List<int> zipCodes = new List<int>();

            connection.ConnectionString = ConnectionString;
            connection.Open();

            SqlCommand commandCheckZip = new SqlCommand("SELECT Zip FROM Cities");
            commandCheckZip.Connection = connection;
            SqlDataReader readerCheckZip = commandCheckZip.ExecuteReader();

            while (readerCheckZip.Read())
            {
                zipCodes.Add(readerCheckZip.GetInt32(0));
            }

            foreach (int zip in zipCodes) //provjera postoji li ZipCode u DB i hoće li se odobriti unos igrača u DB
            {
                if (zip == player.PlaceOfResidence.ZipCode)
                {
                    readerCheckZip.Close();
                    SqlCommand commandAddToDb = new SqlCommand(
                        "INSERT INTO Players (Id, Name, Surname, City) VALUES (@id, @name, @surname, @city);");
                    commandAddToDb.Parameters.AddWithValue("@id", player.Id);
                    commandAddToDb.Parameters.AddWithValue("@name", player.Name);
                    commandAddToDb.Parameters.AddWithValue("@surname", player.Surname);
                    commandAddToDb.Parameters.AddWithValue("@city", player.PlaceOfResidence.ZipCode);
                    commandAddToDb.Connection = connection;
                    commandAddToDb.ExecuteNonQuery();
                    connection.Close();
                }
            }
            connection.Close();
        }

        [HttpGet]
        public static List<Player> GetAllData()
        {
            List<Player> team = new List<Player>();
            SqlConnection connection = new SqlConnection();

            connection.ConnectionString = ConnectionString;
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT Players.Id, Players.Name, Players.Surname, Players.City, Cities.CityName FROM Players, Cities WHERE Players.City = Cities.Zip ");
            command.Connection = connection;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    team.Add(new Player(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), new City(reader.GetInt32(3), reader.GetString(4))));
                }
                connection.Close();
                return team;
            }
            else
            {
                connection.Close();
                return null;
            }
        }

        [HttpPut]
        public static void UpdatePlayer([FromBody] Player player, int id)
        {
            if (id != player.Id)
            {
                return;
            }

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionString;
            connection.Open();

            SqlCommand command = new SqlCommand("UPDATE Players SET Name = @name, Surname = @surname, City = @city WHERE Id =" + id + "");
            command.Parameters.AddWithValue("@name", player.Name);
            command.Parameters.AddWithValue("@surname", player.Surname);
            command.Parameters.AddWithValue("@city", player.PlaceOfResidence.ZipCode);
            command.Connection = connection;
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void Delete(int id)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionString;
            connection.Open();

            SqlCommand command = new SqlCommand("DELETE FROM Players WHERE Id =" + id + "");
            command.Connection = connection;
            command.ExecuteNonQuery();
        }
    }
}