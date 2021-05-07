using Models;
using Repository;
using Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace Services
{
    public class CityService : ICityService
    {
        CityRepository repo = new CityRepository();

        public async Task<int> InsertDataAsync(int zipCode, string name)
        {
            int result = await repo.InsertDataAsync(zipCode, name);
            return result;
        }

        public async Task<City> GetDataAsync(int zipCode)
        {
            City result = await repo.GetDataAsync(zipCode);
            return result;
        }

        public async Task<int> UpdateDataAsync(int zipCode, string name)
        {
            int result = await repo.UpdateDataAsync(zipCode, name);
            return result;
        }

        public async Task<int> DeleteDataAsync(int zipCode)
        {
            int result = await repo.DeleteDataAsync(zipCode);
            return result;
        }
    }
}