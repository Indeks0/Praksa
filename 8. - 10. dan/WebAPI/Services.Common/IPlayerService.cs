using Models.Common;
using Project.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common
{
    public interface IPlayerService
    {
        Task<int> AddToDBAsync(IPlayer player);
        Task<List<IPlayer>> GetAllDataAsync(ICustomDBQuery customDBQuery);
        Task<int> UpdatePlayerAsync(IPlayer player, Guid id);
        Task<int> DeleteAsync(int id);
    }
}
