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
    public class CityRepository
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;


        [HttpPost]
        public static void InsertData(int zipCode, [FromBody] string name)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionString;
            connection.Open();

            SqlCommand command = new SqlCommand(
                "INSERT INTO Cities (Zip, CityName) VALUES(@zipCode, @cityName)");
            command.Parameters.AddWithValue("@zipCode", zipCode);
            command.Parameters.AddWithValue("@cityName", name);
            command.Connection = connection;
            command.ExecuteNonQuery();
            connection.Close();
        }

        [HttpGet]
        public static City GetData(int zipCode)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionString;
            connection.Open();

            SqlCommand command = new SqlCommand(
                "SELECT * FROM Cities WHERE Zip = @zipCode");
            command.Parameters.AddWithValue("@zipCode", zipCode);
            command.Connection = connection;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                City city = new City(reader.GetInt32(0), reader.GetString(1));
                connection.Close();
                return city;
            }
            connection.Close();
            return null;

        }

        [HttpPut]
        public static void UpdateData(int zipCode, [FromBody] string name)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionString;
            connection.Open();

            SqlCommand command = new SqlCommand(
                "UPDATE Cities SET CityName = @cityName WHERE Zip=" + zipCode + "");
            command.Parameters.AddWithValue("@cityName", name);
            command.Connection = connection;
            command.ExecuteNonQuery();

            connection.Close();           
        }

        [HttpDelete]
        public static void DeleteData(int zipCode)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionString;
            connection.Open();

            SqlCommand command = new SqlCommand(
                "DELETE FROM Cities WHERE Zip=" + zipCode + "");
            command.Connection = connection;
            command.ExecuteNonQuery();
            connection.Close();

        }
    }
}
