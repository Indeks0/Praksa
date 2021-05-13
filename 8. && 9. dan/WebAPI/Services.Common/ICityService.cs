using Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common
{
    public interface ICityService
    {
        Task<int> InsertDataAsync(int zipCode, string name);
        Task<ICity> GetDataAsync(int zipCode);
        Task<List<ICity>> GetAllDataAsync(CitySort citySort);
        Task<int> UpdateDataAsync(int zipCode, string name);
        Task<int> DeleteDataAsync(int zipCode);
    }
}
