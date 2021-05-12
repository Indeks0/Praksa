using Models;
using Models.Common;
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
    public class PlayerService : IPlayerService
    {
        IPlayerRepository Repo { get; set; }

        public PlayerService()
        {

        }
        public PlayerService(IPlayerRepository repo)
        {
            this.Repo = repo;
        }

        public async Task<int> AddToDBAsync(IPlayer player)
        {
            int result = await Repo.AddToDBAsync(player);
            return result;
        }

        public async Task<List<IPlayer>> GetAllDataAsync()
        {
            List<IPlayer> result = await Repo.GetAllDataAsync();
            return result;
        }

        public async Task<int> UpdatePlayerAsync(IPlayer player, int id)
        {
            int result = await Repo.UpdatePlayerAsync(player, id);
            return result;
        }

        public async Task<int> DeleteAsync(int id)
        {
            int result = await Repo.DeleteAsync( id);
            return result;
        }
    }
}