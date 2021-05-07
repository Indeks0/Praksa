using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common
{
    public interface IPlayerService
    {
        //Task<int> AddToDBAsync(IPlayer player);            DI rjesenje
        //Task<List<IPlayer>> GetAllDataAsync(               DI rjesenje
        //Task<int> UpdatePlayerAsync(Player player, int id) DI rjesenje
        Task<int> DeleteAsync(int id);
    }
}
