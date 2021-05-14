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
        public async Task<List<ICity>> GetAllDataAsync([FromBody] ICustomDBQuery customDBQuery)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionString;
            connection.Open();

            SqlCommand command = new SqlCommand(
                "SELECT * FROM Cities");
            if (customDBQuery.Filtering.AttributeToFilter != "default" && customDBQuery.Filtering.AttributeValue.Contains("%"))
            {
                command.CommandText += " WHERE " + customDBQuery.Filtering.AttributeToFilter + " LIKE '" + customDBQuery.Filtering.AttributeValue + "'";
            }
            else if(customDBQuery.Filtering.AttributeToFilter != "default")
            {
                command.CommandText += " WHERE " + customDBQuery.Filtering.AttributeToFilter + " = '" + customDBQuery.Filtering.AttributeValue + "'";
            }
            else
            {
                command.CommandText += " WHERE CityName IS NOT NULL";
            }
            if (customDBQuery.Sorting.AttributeToSort != "default")
            {
                command.CommandText += " ORDER BY " + customDBQuery.Sorting.AttributeToSort + " " + customDBQuery.Sorting.SortingOrder;
            }
            else
            {
                command.CommandText += " ORDER BY CityName ASC";
            }
            command.CommandText += " OFFSET " + (customDBQuery.Paging.CurrentPage * customDBQuery.Paging.ItemsPerPage) + " ROWS FETCH NEXT " + customDBQuery.Paging.ItemsPerPage + " ROWS ONLY ";
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
