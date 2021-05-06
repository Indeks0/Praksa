using CRUD.DataClasses;
using CRUD.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CRUD.Services
{
    public class PlayerService
    {
        public static void AddToDB([FromBody] Player player)
        {
            PlayerRepository.AddToDB(player);
        }

        public static List<Player> GetAllData()
        {
            return PlayerRepository.GetAllData();
        }

        public static void UpdatePlayer([FromBody] Player player, int id)
        {
            PlayerRepository.UpdatePlayer(player, id);
        }

        public static void Delete(int id)
        {
            PlayerRepository.Delete(id);
        }
    }
}