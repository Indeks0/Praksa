using Models;
using Models.Common;
using Project.Common.Interfaces;
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
        public async Task<List<IPlayer>> GetAllDataAsync([FromBody] ICustomDBQuery customDBQuery)
        {
            List<IPlayer> team = new List<IPlayer>();
            SqlConnection connection = new SqlConnection();

            connection.ConnectionString = ConnectionString;
            connection.Open();

            SqlCommand command = new SqlCommand(
                "SELECT Players.Id, Players.Name, Players.Surname, Players.CityId, Cities.CityName FROM Players, Cities");
            if (customDBQuery.Filtering.AttributeToFilter != "default" && customDBQuery.Filtering.AttributeValue.Contains("%"))
            {
                command.CommandText += " WHERE " + customDBQuery.Filtering.AttributeToFilter + " LIKE '" + customDBQuery.Filtering.AttributeValue + "' AND Players.CityId = Cities.ZipCode"; // "AND Players.CityId = Cities.ZipCode" -> potrebno zbog dohvaćanja CityName za svaki CityId
            }                                                                                                                                                                                // Može se filtrirati i po CityName ili CityId
            else if(customDBQuery.Filtering.AttributeToFilter != "default")
            {
                command.CommandText += " WHERE " + customDBQuery.Filtering.AttributeToFilter + " = '" + customDBQuery.Filtering.AttributeValue + "' AND Players.CityId = Cities.ZipCode";
            }
            else //default filtering settings
            {
                command.CommandText += " WHERE Name IS NOT NULL AND Players.CityId = Cities.ZipCode";
            }
            if (customDBQuery.Sorting.AttributeToSort != "default")
            {
                command.CommandText += " ORDER BY " + customDBQuery.Sorting.AttributeToSort + " " + customDBQuery.Sorting.SortingOrder;
            }
            else //default sorting settings
            {
                command.CommandText += " ORDER BY CityName ASC";
            }
            command.CommandText += " OFFSET " + (customDBQuery.Paging.CurrentPage * customDBQuery.Paging.ItemsPerPage) + " ROWS FETCH NEXT " + customDBQuery.Paging.ItemsPerPage + " ROWS ONLY ";
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