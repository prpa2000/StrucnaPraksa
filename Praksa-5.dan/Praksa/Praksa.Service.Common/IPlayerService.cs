
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Praksa.Common;
using Praksa.Model;

namespace Praksa.Service.Common
{
    public interface IPlayerService
    {
        Task<List<Player>> GetAllPlayersAsync(Paging paging, Sorting sorting, PlayerFiltering filters);
        Task<Player> GetPlayerByIdAsync(int id);
        Task CreatePlayerAsync(CreatedPlayer player);
        Task UpdatePlayerAsync(int id, UpdatedPlayer player);
        Task DeletePlayerAsync(int id);
    }
}
