using Models;
using Models.Common;
using Repository;
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
    public class CityController : ApiController
    {
        ICityService Service { get; set; }

        public CityController()
        {}
        public CityController(ICityService service)
        {
            this.Service = service;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> InsertDataAsync(int zipCode, [FromBody] string name)
        {
            int result = await Service.InsertDataAsync(zipCode, name);
            if (result == 1)
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"Added the city with zipCode {zipCode} to the DB.");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, $"City with zipCode {zipCode} was not added to the DB.");
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetDataAsync(int zipCode)
        {
            ICity resultCity = await Service.GetDataAsync(zipCode);
            if (resultCity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, resultCity);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, $"The City with zipcode {zipCode} was not found in DB.");
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateDataAsync(int zipCode, [FromBody] string name)
        {
            int result = await Service.UpdateDataAsync(zipCode, name);
            if (result == 1)
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"Updated the city with zipCode {zipCode} in the DB.");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, $"City with zipCode {zipCode} was not found and updated in the DB.");
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteDataAsync(int zipCode)
        {
            int result = await Service.DeleteDataAsync(zipCode);
            if (result == 1)
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"Deleted the city with zipCode {zipCode} from the DB.");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, $"City with zipCode {zipCode} was not found and deleted in the DB.");
        }
    }
}
