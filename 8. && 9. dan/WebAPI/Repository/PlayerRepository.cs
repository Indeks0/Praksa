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
        public async Task<int> AddToDBAsync(IPlayer player)
        {
            SqlConnection connection = new SqlConnection();
            List<int> zipCodes = new List<int>();

            connection.ConnectionString = ConnectionString;
            connection.Open();

            SqlCommand commandCheckZip = new SqlCommand("SELECT ZipCode FROM Cities WHERE ZipCode = '" + player.PlaceOfResidence.ZipCode + "'");
            commandCheckZip.Connection = connection;
            SqlDataReader readerCheckZip = commandCheckZip.ExecuteReader();

            if ((readerCheckZip.HasRows))
            {
                player.Id = Guid.NewGuid();
                int i = player.Id.ToString().Length;
                readerCheckZip.Close();
                SqlCommand commandAddToDb = new SqlCommand(
                    "INSERT INTO Players (Id, Name, Surname, CityId) VALUES (@id, @name, @surname, @cityId);");
                commandAddToDb.Parameters.AddWithValue("@id", player.Id);
                commandAddToDb.Parameters.AddWithValue("@name", player.Name);
                commandAddToDb.Parameters.AddWithValue("@surname", player.Surname);
                commandAddToDb.Parameters.AddWithValue("@cityId", player.PlaceOfResidence.ZipCode);
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
            return 0;
        }

        [HttpGet]
        public async Task<List<IPlayer>> GetAllDataAsync(PlayerSort playerSort)
        {
            List<IPlayer> team = new List<IPlayer>();
            SqlConnection connection = new SqlConnection();

            connection.ConnectionString = ConnectionString;
            connection.Open();

            SqlCommand command = new SqlCommand(
                "SELECT Players.Id, Players.Name, Players.Surname, Players.CityId, Cities.CityName FROM Players, Cities");
            if (playerSort.FilterBy != "")
            {
                command.CommandText += " WHERE Name = '" + playerSort.FilterBy + "' AND Players.CityId = Cities.ZipCode";
            }
            else
            {
                command.CommandText += " WHERE Name IS NOT NULL AND Players.CityId = Cities.ZipCode";
            }
            if (playerSort.SortBy != "")
            {
                command.CommandText += " ORDER BY " + playerSort.SortBy + " " + playerSort.SortingOrder;
            }
            else
            {
                command.CommandText += " ORDER BY CityName ASC";
            }
            command.CommandText += " OFFSET " + (playerSort.PageNumber * 5) + " ROWS FETCH NEXT " + 5 + " ROWS ONLY ";
            command.Connection = connection;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    team.Add(new Player(Guid.Parse(reader.GetString(0)), reader.GetString(1), reader.GetString(2), new City(reader.GetInt32(3), reader.GetString(4))));
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
        public async Task<int> UpdatePlayerAsync(IPlayer player, Guid id)
        {
            if (id != player.Id)
            {
                return 0;
            }

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionString;
            connection.Open();

            SqlCommand command = new SqlCommand("UPDATE Players SET Name = @name, Surname = @surname, CityId = @city WHERE Id =" + id + "");
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