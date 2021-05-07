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
    public class PlayerService : IPlayerService
    {
        PlayerRepository repo = new PlayerRepository();

        public async Task<int> AddToDBAsync(Player player)
        {
            int result = await repo.AddToDBAsync(player);
            return result;
        }

        public async Task<List<Player>> GetAllDataAsync()
        {
            List<Player> result = await repo.GetAllDataAsync();
            return result;
        }

        public async Task<int> UpdatePlayerAsync(Player player, int id)
        {
            int result = await repo.UpdatePlayerAsync(player, id);
            return result;
        }

        public async Task<int> DeleteAsync(int id)
        {
            int result = await repo.DeleteAsync( id);
            return result;
        }
    }
}