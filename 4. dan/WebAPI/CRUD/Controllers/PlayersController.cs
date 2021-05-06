using CRUD.DataClasses;
using CRUD.Services;
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
            PlayerService.AddToDB(player);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        public HttpResponseMessage GetAllData()
        {
            return Request.CreateResponse(HttpStatusCode.OK, PlayerService.GetAllData());
        }

        [HttpPut]
        public HttpResponseMessage UpdatePlayer([FromBody] Player player, int id)
        {
            PlayerService.UpdatePlayer(player, id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Delete(int id)
        {
            PlayerService.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

