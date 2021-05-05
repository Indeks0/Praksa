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
    public class PlayersController : ApiController
    {
        public string ConnectionString = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;

        [HttpPost]
        public HttpResponseMessage AddToDB([FromBody] Player player)
        {
            SqlConnection connection = new SqlConnection();
            List<int> zipCodes = new List<int>();

            connection.ConnectionString = this.ConnectionString;
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
                    return Request.CreateResponse(HttpStatusCode.OK, "Added the player to DB.");
                }
            }
            connection.Close();
            return Request.CreateResponse(HttpStatusCode.BadRequest, $"Players PlaceOfResidence is not in DB, please add it to DB and after that add the player.");
        }

        [HttpGet]
        public HttpResponseMessage GetAllData()
        {
            List<Player> team = new List<Player>();
            SqlConnection connection = new SqlConnection();

            connection.ConnectionString = this.ConnectionString;
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
                return Request.CreateResponse(HttpStatusCode.OK, team);
            }
            else
            {
                connection.Close();
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Players DB is empty");
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdatePlayer([FromBody] Player player, int id)
        {
            if (id != player.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Can't change the Id of a player.");
            }

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = this.ConnectionString;
            connection.Open();

            SqlCommand command = new SqlCommand("UPDATE Players SET Name = @name, Surname = @surname, City = @city WHERE Id =" + id + "");
            command.Parameters.AddWithValue("@name", player.Name);
            command.Parameters.AddWithValue("@surname", player.Surname);
            command.Parameters.AddWithValue("@city", player.PlaceOfResidence.ZipCode);
            command.Connection = connection;
            command.ExecuteNonQuery();
            connection.Close();

            return Request.CreateResponse(HttpStatusCode.OK, "Updated the player in DB.");
        }

        public HttpResponseMessage Delete(int id)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = this.ConnectionString;
            connection.Open();

            SqlCommand command = new SqlCommand("DELETE FROM Players WHERE Id =" + id + "");
            command.Connection = connection;
            command.ExecuteNonQuery();

            return Request.CreateResponse(HttpStatusCode.OK, "Deleted the player from DB."); //nisam radio provjeru
        }
    }
}

