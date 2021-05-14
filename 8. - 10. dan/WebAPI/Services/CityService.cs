using Models;
using Models.Common;
using Project.Common.Interfaces;
using Repository;
using Repository.Common;
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
        ICityRepository Repo { get; set; }

        public CityService()
        {

        }
        public CityService(ICityRepository repo)
        {
            this.Repo = repo;
        }

        public async Task<int> InsertDataAsync(int zipCode, string name)
        {
            int result = await Repo.InsertDataAsync(zipCode, name);
            return result;
        }

        public async Task<ICity> GetDataAsync(int zipCode)
        {
            ICity result = await Repo.GetDataAsync(zipCode);
            return result;
        }

        public async Task<int> UpdateDataAsync(int zipCode, string name)
        {
            int result = await Repo.UpdateDataAsync(zipCode, name);
            return result;
        }

        public async Task<int> DeleteDataAsync(int zipCode)
        {
            int result = await Repo.DeleteDataAsync(zipCode);
            return result;
        }

        public async Task<List<ICity>> GetAllDataAsync(ICustomDBQuery customDBQuery)
        {
            List<ICity> result = await Repo.GetAllDataAsync(customDBQuery);
            return result;
        }
    }
}