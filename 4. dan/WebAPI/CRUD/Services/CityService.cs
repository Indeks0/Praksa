using CRUD.DataClasses;
using CRUD.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CRUD.Services
{
    public class CityService
    {
        public static void InsertData(int zipCode, [FromBody] string name)
        {
            CityRepository.InsertData(zipCode, name);
        }

        public static City GetData(int zipCode)
        {
            return CityRepository.GetData(zipCode);
        }

        public static void UpdateData(int zipCode, [FromBody] string name)
        {
            CityRepository.UpdateData(zipCode, name);
        }

        public static void DeleteData(int zipCode)
        {
            CityRepository.DeleteData(zipCode);
        }
    }
}