using Praksa.Common;
using Praksa.Model;
using Praksa.Repository.Common;
using Praksa.Service.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Praksa.Service
{
    public class PlayerService : IPlayerService
    {
        IPlayerRepository PlayerRepository { get; set; }
        public PlayerService(IPlayerRepository playerRepository)
        {
           PlayerRepository = playerRepository;
        }

        public async Task<List<Player>> GetAllPlayersAsync(Paging paging, Sorting sorting, PlayerFiltering filters)
        {
            try
            {
                return await PlayerRepository.GetAllPlayersAsync(paging, sorting, filters);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task<Player> GetPlayerByIdAsync(int id)
        {
            try
            {
                return await PlayerRepository.GetPlayerByIdAsync(id);
            }
            catch { return null; }
        }
        public async Task CreatePlayerAsync(CreatedPlayer player)
        {
            try
            {
               await PlayerRepository.CreatePlayerAsync(player);
               
            }
            catch(Exception e) {
                throw e;
            }
        }
        public async Task UpdatePlayerAsync(int id, UpdatedPlayer player)
        {
            try
            {
              await PlayerRepository.UpdatePlayerAsync(id, player);
               
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task DeletePlayerAsync(int id)
        {
            try
            {
               await PlayerRepository.DeletePlayerAsync(id);
               
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
