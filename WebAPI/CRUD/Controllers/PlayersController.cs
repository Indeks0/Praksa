using CRUD.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CRUD.Controllers
{
    public class PlayersController : ApiController
    {
        static List<Player> team = new List<Player>() {
            new Player("Unknown", "Unknown"),
            new Player("Unknown", "Unknown")
        };

        public HttpResponseMessage Post([FromBody] Player player)
        {
            team.Add(player);

            HttpResponseMessage response;
            response = Request.CreateResponse(HttpStatusCode.Created, $"Added the player {player.Name} {player.Surname} to the team.");
            return response;
        }

        public HttpResponseMessage Get()
        {
            HttpResponseMessage response;
            if (team.Count() > 0)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, team);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NoContent, "Team has no players.");
            }
            return response;
        }

        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response;
            if (team.Count() <= id)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "The index is out of bounds.");
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, team[id]);
            }
            return response;
        }

        [HttpPut]
        public HttpResponseMessage Put([FromBody] Player player, int id)
        {
            HttpResponseMessage response;

            if (team.Count <= id)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "The index is out of bounds.");
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, $"Updated the player at index {id} in the team.");
                team[id] = player;
            }
            return response;
        }

        public HttpResponseMessage Delete([FromBody] Player player)
        {
            HttpResponseMessage response;
            int flag = 0;

            foreach(Player toDelete in team)
            {
                if(toDelete.Name == player.Name && toDelete.Surname == player.Surname)
                {
                    team.Remove(toDelete);
                    flag = 1;
                    break;
                }
            }
            if (flag == 1)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, $"Removed the player {player.Name} {player.Surname} from the team.");
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, $"The player {player.Name} {player.Surname} was not found in the team.");
            }

            return response;
        }
    }
}
