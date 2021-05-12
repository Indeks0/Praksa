using Models;
using Models.Common;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Repository
{
    public class PlayerRepository : IPlayerRepository
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;

        [HttpPost]
        public async Task<int> AddToDBAsync(IPlayer player) //PROMJENITI SELECT, UKLONITI LISTU I NAPRAVITI U SELECTU
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
                    int result;
                    try
                    {
                        result = await commandAddToDb.ExecuteNonQueryAsync();
                        connection.Close();
                        return result;
                    }
                    catch (Exception exception)
                    {
                        return 0;
                    }
                }
            }
            return 0;
        }

        [HttpGet]
        public async Task<List<IPlayer>> GetAllDataAsync()
        {
            List<IPlayer> team = new List<IPlayer>();
            SqlConnection connection = new SqlConnection();

            connection.ConnectionString = ConnectionString;
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT Players.Id, Players.Name, Players.Surname, Players.City, Cities.CityName FROM Players, Cities WHERE Players.City = Cities.Zip ");
            command.Connection = connection;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
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
        public async Task<int> UpdatePlayerAsync(IPlayer player, int id)
        {
            if (id != player.Id)
            {
                return 0;
            }

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionString;
            connection.Open();

            SqlCommand command = new SqlCommand("UPDATE Players SET Name = @name, Surname = @surname, City = @city WHERE Id =" + id + "");
            command.Parameters.AddWithValue("@name", player.Name);
            command.Parameters.AddWithValue("@surname", player.Surname);
            command.Parameters.AddWithValue("@city", player.PlaceOfResidence.ZipCode);
            command.Connection = connection;
            int result;

            try
            {
                result = await command.ExecuteNonQueryAsync();
                connection.Close();
                return result;
            }
            catch (Exception exception)
            {
                return 0;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionString;
            connection.Open();

            SqlCommand command = new SqlCommand("DELETE FROM Players WHERE Id =" + id + "");
            command.Connection = connection;
            int result;

            try
            {
                result = await command.ExecuteNonQueryAsync();
                connection.Close();
                return result;
            }
            catch (Exception exception)
            {
                return 0;
            }
        }
    }
}