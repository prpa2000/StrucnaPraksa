using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Praksa.Common;
using Praksa.Model;




namespace Praksa.Repository.Common
{
    public interface IPlayerRepository
    {
        Task<List<Player>> GetAllPlayersAsync(Paging paging, Sorting sorting, PlayerFiltering filters);
        Task<Player> GetPlayerByIdAsync(int id);
        Task<HttpResponseMessage> CreatePlayerAsync(CreatedPlayer player);
        Task<HttpResponseMessage> UpdatePlayerAsync(int id, UpdatedPlayer player);
        Task<HttpResponseMessage> DeletePlayerAsync(int id);
    }
}
