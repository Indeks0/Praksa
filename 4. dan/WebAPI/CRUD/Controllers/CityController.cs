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
    public class CityController : ApiController
    {
        public string ConnectionString = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;

        [HttpPost]
        public HttpResponseMessage InsertData(int zipCode, [FromBody] string name)
        {
            CityService.InsertData(zipCode, name);
            return Request.CreateResponse(HttpStatusCode.OK, $"Added the city with zipCode {zipCode} to the DB.");
        }

        [HttpGet]
        public HttpResponseMessage GetData(int zipCode)
        {

            return Request.CreateResponse(HttpStatusCode.OK, CityService.GetData(zipCode));
        }

        [HttpPut]
        public HttpResponseMessage UpdateData(int zipCode, [FromBody] string name)
        {
            CityService.UpdateData(zipCode, name);
            return Request.CreateResponse(HttpStatusCode.OK, $"Updated the city with zipCode {zipCode}");
        }

        [HttpDelete]
        public HttpResponseMessage DeleteData(int zipCode)
        {
            CityService.DeleteData(zipCode);
            return Request.CreateResponse(HttpStatusCode.OK, $"Deleted city from DB with zipCode {zipCode}");
        }
    }
}
