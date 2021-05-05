using CRUD.DataClasses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CRUD.Controllers
{
    public class CityController : ApiController
    {
        public string ConnectionString = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;

        [HttpPost]
        public HttpResponseMessage InsertData(int zipCode, [FromBody] string name)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = this.ConnectionString;
            connection.Open();

            SqlCommand command = new SqlCommand(
                "INSERT INTO Cities (Zip, CityName) VALUES(@zipCode, @cityName)");
            command.Parameters.AddWithValue("@zipCode", zipCode);
            command.Parameters.AddWithValue("@cityName", name);
            command.Connection = connection;
            command.ExecuteNonQuery();
            connection.Close();
            return Request.CreateResponse(HttpStatusCode.OK, $"Added the city with zipCode {zipCode} to the DB.");
        }

        [HttpGet]
        public HttpResponseMessage GetData(int zipCode)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = this.ConnectionString;
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
                return Request.CreateResponse(HttpStatusCode.OK, city);
            }
            connection.Close();
            return Request.CreateResponse(HttpStatusCode.BadRequest, $"No city with zipCode {zipCode} found in DB.");

        }

        [HttpPut]
        public HttpResponseMessage UpdateData(int zipCode, [FromBody] string name)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = this.ConnectionString;
            connection.Open();

            SqlCommand command = new SqlCommand(
                "UPDATE Cities SET CityName = @cityName WHERE Zip=" + zipCode + "");
            command.Parameters.AddWithValue("@cityName", name);
            command.Connection = connection;
            command.ExecuteNonQuery();

            connection.Close();
            return Request.CreateResponse(HttpStatusCode.OK, $"Updated the city with zipCode {zipCode}");
        }

        [HttpDelete]
        public HttpResponseMessage DeleteData(int zipCode)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = this.ConnectionString;
            connection.Open();

            SqlCommand command = new SqlCommand(
                "DELETE FROM Cities WHERE Zip=" + zipCode + "");
            command.Connection = connection;
            command.ExecuteNonQuery();
            connection.Close();

            return Request.CreateResponse(HttpStatusCode.OK, $"Deleted city from DB with zipCode {zipCode}");
        }
    }
}
