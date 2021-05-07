using Models;
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
                "INSERT INTO Cities (Zip, CityName) VALUES(@zipCode, @cityName)");
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
        public async Task<City> GetDataAsync(int zipCode)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionString;
            connection.Open();

            SqlCommand command = new SqlCommand(
                "SELECT * FROM Cities WHERE Zip = @zipCode");
            command.Parameters.AddWithValue("@zipCode", zipCode);
            command.Connection = connection;

            SqlDataReader reader = command.ExecuteReader();

            bool result = await reader.ReadAsync();

            if (result)
            {
                City city = new City(reader.GetInt32(0), reader.GetString(1));
                connection.Close();
                return city;
            }
            connection.Close();
            return null;

        }

        [HttpPut]
        public async Task<int> UpdateDataAsync(int zipCode, string name)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionString;
            connection.Open();

            SqlCommand command = new SqlCommand(
                "UPDATE Cities SET CityName = @cityName WHERE Zip=" + zipCode + "");
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
                "DELETE FROM Cities WHERE Zip=" + zipCode + "");
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
