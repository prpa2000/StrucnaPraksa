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

        public async Task<List<Player>> GetAllPlayersAsync()
        {
            try
            {
                return await playerRepository.GetAllPlayersAsync();
            }
            catch
            {
                return null;
            }
        }
        public async Task<Player> GetPlayerByIdAsync(int id)
        {
            try
            {
                return await playerRepository.GetPlayerByIdAsync(id);
            }
            catch { return null; }
        }
        public async Task CreatePlayerAsync(CreatedPlayer player)
        {
            try
            {
               await playerRepository.CreatePlayerAsync(player);
            }
            catch {
                throw;
            }
        }
        public async Task UpdatePlayerAsync(int id, UpdatedPlayer player)
        {
            try
            {
               await playerRepository.UpdatePlayerAsync(id, player);
            }
            catch
            {
                throw;
            }
        }
        public async Task DeletePlayerAsync(int id)
        {
            try
            {
                await playerRepository.DeletePlayerAsync(id);
            }
            catch
            {
                throw;
            }
        }
    }
}
