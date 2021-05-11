using Models;
using Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Common
{
    public interface IPlayerRepository
    {
        Task<int> AddToDBAsync(IPlayer player);
        Task<List<IPlayer>> GetAllDataAsync();
        Task<int> UpdatePlayerAsync(IPlayer player, int id);
        Task<int> DeleteAsync(int id);
    }
}
