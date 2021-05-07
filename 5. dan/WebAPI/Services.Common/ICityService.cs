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
        //Task<ICity> GetDataAsync(int zipCode)     DI rjesenje
        Task<int> UpdateDataAsync(int zipCode, string name);
        Task<int> DeleteDataAsync(int zipCode);
    }
}
