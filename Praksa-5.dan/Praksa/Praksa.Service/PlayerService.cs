using Praksa.Model;
using Praksa.Repository.Common;
using Praksa.Service.Common;
using Praksa.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praksa.Service
{
    public class PlayerService : IPlayerService
    {
        IPlayerRepository playerRepository;
        public PlayerService(IPlayerRepository playerRepository)
        {
            this.playerRepository = playerRepository;
        }

        public List<Player> GetAllPlayers()
        {
            try
            {
                return playerRepository.GetAllPlayers();
            }
            catch
            {
                return null;
            }
        }
        public Player GetPlayerById(int id)
        {
            try
            {
                return playerRepository.GetPlayerById(id);
            }
            catch { return null; }
        }
        public void CreatePlayer(CreatedPlayer player)
        {
            try
            {
                playerRepository.CreatePlayer(player);
            }
            catch {
                throw;
            }
        }
        public void UpdatePlayer(int id, UpdatedPlayer player)
        {
            try
            {
                playerRepository.UpdatePlayer(id, player);
            }
            catch
            {
                throw;
            }
        }
        public void DeletePlayer(int id)
        {
            try
            {
                playerRepository.DeletePlayer(id);
            }
            catch
            {
                throw;
            }
        }
    }
}
