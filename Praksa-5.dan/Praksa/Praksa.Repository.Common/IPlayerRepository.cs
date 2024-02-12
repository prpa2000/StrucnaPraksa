using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praksa.Model;




namespace Praksa.Repository.Common
{
    public interface IPlayerRepository
    {
        Task<List<Player>> GetAllPlayersAsync();
        Task<Player> GetPlayerByIdAsync(int id);
        Task CreatePlayerAsync(CreatedPlayer player);
        Task UpdatePlayerAsync(int id, UpdatedPlayer player);
        Task DeletePlayerAsync(int id);
    }
}
