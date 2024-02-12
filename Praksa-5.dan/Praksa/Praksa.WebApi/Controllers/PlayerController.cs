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
        IPlayerService PlayerServicer { get; set; }
       public PlayerController(IPlayerService playerServicer)
        {
            PlayerServicer = playerServicer;
        }

        public async Task<List<Player>> GetAllPlayersAsync()
        {
            try
            {
                return await PlayerServicer.GetAllPlayersAsync();
            }
            catch {
                throw;
            }
        }

        public async Task<Player> GetPlayerByIdAsync(int id)
        {
            try
            {
                return await PlayerServicer.GetPlayerByIdAsync(id);
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
                 await PlayerServicer.CreatePlayerAsync(player);
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
                await PlayerServicer.UpdatePlayerAsync(id, player);
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
                await PlayerServicer.DeletePlayerAsync(id);
            }
            catch
            {
                throw;
            }
        }
    }
}
