using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Common
{
    public interface IPlayerRepository
    {
        //Task<int> AddToDBAsync(IPlayer player);               DI rjesnje
        //Task<List<Player>> GetAllDataAsync();                 DI rjesenje
        //UpdatePlayerAsync(Player player, int id)              DI rjesenje
        Task<int> DeleteAsync(int id);
    }
}
