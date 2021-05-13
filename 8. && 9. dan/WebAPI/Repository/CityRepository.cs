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
    public class CityRepository : ICityRepository
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;

        [HttpPost]
        public async Task<int> InsertDataAsync(int zipCode, string name)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionString;
            connection.Open();

            SqlCommand command = new SqlCommand(
                "INSERT INTO Cities (ZipCode, CityName) VALUES(@zipCode, @cityName)");
            command.Parameters.AddWithValue("@zipCode", zipCode);
            command.Parameters.AddWithValue("@cityName", name);
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

        [HttpGet]
        public async Task<ICity> GetDataAsync(int zipCode)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionString;
            connection.Open();

            SqlCommand command = new SqlCommand(
                "SELECT * FROM Cities WHERE ZipCode = @zipCode");
            command.Parameters.AddWithValue("@zipCode", zipCode);
            command.Connection = connection;

            SqlDataReader reader = command.ExecuteReader();

            bool result = await reader.ReadAsync();

            if (result)
            {
                ICity city = new City(reader.GetInt32(0), reader.GetString(1));
                connection.Close();
                return city;
            }
            connection.Close();
            return null;

        }

        [HttpGet]
        public async Task<List<ICity>> GetAllDataAsync(CitySort citySort)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionString;
            connection.Open();

            SqlCommand command = new SqlCommand(
                "SELECT * FROM Cities");
            if (citySort.FilterBy != "")
            {
                command.CommandText += " WHERE ZipCode LIKE '" + citySort.FilterBy + "%'";
            }
            else
            {
                command.CommandText += " WHERE CityName IS NOT NULL";
            }
            if (citySort.SortBy != "")
            {
                command.CommandText += " ORDER BY " + citySort.SortBy + " " + citySort.SortingOrder;
            }
            else
            {
                command.CommandText += " ORDER BY CityName ASC";
            }
            command.CommandText += " OFFSET " + (citySort.PageNumber * 5) + " ROWS FETCH NEXT " + 5 + " ROWS ONLY ";
            command.Connection = connection;

            SqlDataReader reader = command.ExecuteReader();
            List<ICity> cities = new List<ICity>();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    cities.Add(new City(reader.GetInt32(0), reader.GetString(1)));
                }
                connection.Close();
                return cities;
            }
            else
            {
                connection.Close();
                return null;
            }

        }

        [HttpPut]
        public async Task<int> UpdateDataAsync(int zipCode, string name)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionString;
            connection.Open();

            SqlCommand command = new SqlCommand(
                "UPDATE Cities SET CityName = @cityName WHERE ZipCode=" + zipCode + "");
            command.Parameters.AddWithValue("@cityName", name);
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

        [HttpDelete]
        public async Task<int> DeleteDataAsync(int zipCode)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionString;
            connection.Open();

            SqlCommand command = new SqlCommand(
                "DELETE FROM Cities WHERE ZipCode=" + zipCode + "");
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
