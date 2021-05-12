using AutoMapper;
using Models;
using Models.Common;
using Services;
using Services.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CRUD.Controllers
{
    public class PlayersController : ApiController
    {
        IPlayerService Service { get; set; }
        private readonly IMapper Mapper;

        public PlayersController(IPlayerService service, IMapper mapper)
        {
            this.Service = service;
            this.Mapper = mapper;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> AddToDBAsync([FromBody] PlayerREST player) 
        {
            int result = await Service.AddToDBAsync(this.Mapper.Map<IPlayer>(player));
            if (result == 1)
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"Added the player to the DB.");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, $"Player was not added to the DB.");
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetAllDataAsync()
        {
            List<IPlayer> result = await Service.GetAllDataAsync();
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, this.Mapper.Map<List<PlayerREST>>(result));
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, $"There are no players in the DB.");
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdatePlayerAsync([FromBody] PlayerREST player, int id) //Potrebno promjeniti logiku unutar repo
        {
            int result = await Service.UpdatePlayerAsync(this.Mapper.Map<IPlayer>(player), id);
            if (result == 1)
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"Updated the player in the DB.");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, $"Player was not updated in the DB.");
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            int result = await Service.DeleteAsync(id);
            if (result == 1)
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"Deleted the player in the DB.");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, $"Player was not found and deleted in the DB.");
        }

        public class PlayerREST{
            public string Name { get; set; }
            public string Surname { get; set; }
            public ICity PlaceOfResidence { get; set; }

            public PlayerREST(string name, string surname, City placeOfResidence)
            {
                this.Name = name;
                this.Surname = surname;
                this.PlaceOfResidence = placeOfResidence;
            }

        }
    }
}

