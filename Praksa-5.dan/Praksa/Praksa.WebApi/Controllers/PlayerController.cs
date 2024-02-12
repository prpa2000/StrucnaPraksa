using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using Npgsql;
using System.Security.Cryptography;
using Microsoft.Ajax.Utilities;
using System.Numerics;
using Praksa.Model;
using Praksa.Service.Common;
using System.Threading.Tasks;

namespace Praksa.WebApi.Controllers
{
    public class PlayerController : ApiController
    {
        IPlayerService playerService;
       


        public async Task<List<Player>> GetAllPlayersAsync()
        {
            try
            {
                return await playerService.GetAllPlayersAsync();
            }
            catch {
                throw;
            }
        }

        public async Task<Player> GetPlayerByIdAsync(int id)
        {
            try
            {
                return await playerService.GetPlayerByIdAsync(id);
            }
            catch
            {
                throw;
            }
        }


        [System.Web.Http.HttpPost]
            public async Task CreatePlayerAsync(CreatedPlayer player)
            {
            try
            {
                 await playerService.CreatePlayerAsync(player);
            }
            catch
            {
                throw;
            }
            }


        [System.Web.Http.HttpPut]
        public async Task UpdatePlayerAsync(int id, UpdatedPlayer player)
        {
            try
            {
                await playerService.UpdatePlayerAsync(id, player);
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
                await playerService.DeletePlayerAsync(id);
            }
            catch
            {
                throw;
            }
        }
    }
}
