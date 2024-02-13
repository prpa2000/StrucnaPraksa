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
using Praksa.Common;

namespace Praksa.WebApi.Controllers
{
    public class PlayerController : ApiController
    {
        IPlayerService PlayerServicer { get; set; }
       public PlayerController(IPlayerService playerServicer)
        {
            PlayerServicer = playerServicer;
        }

        public async Task<List<Player>> GetAllPlayersAsync(int? pagenumber = null, int? pagesize = null, string sortby = "", string sortorder = "", int? id = null, string firstname = "", string lastname = "", int? age = null, int? footballclubid = null)
        {
            try
            {
                Paging paging = new Paging(pagenumber, pagesize);
                Sorting sorting = new Sorting(sortby, sortorder);
                PlayerFiltering filters = new PlayerFiltering(id, firstname, lastname, age, footballclubid);
                return await PlayerServicer.GetAllPlayersAsync(paging, sorting, filters);
            }
            catch(Exception e) {
                throw e;
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
            public async Task<HttpResponseMessage> CreatePlayerAsync(CreatedPlayer player)
            {
            if(player == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Player cannot be null");
            }
            
            try
            {
                await PlayerServicer.CreatePlayerAsync(player);
                return Request.CreateResponse(HttpStatusCode.OK, "Player created!");
                
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
            }


        [System.Web.Http.HttpPut]
        public async Task<HttpResponseMessage> UpdatePlayerAsync(int id, UpdatedPlayer player)
        {
            if(player == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Player cannot be null object");
            }
           
            try
            {
                await PlayerServicer.UpdatePlayerAsync(id, player);
                return Request.CreateResponse(HttpStatusCode.OK, "Player updated!");
                
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,e.Message);
            }
        }

        public async Task<HttpResponseMessage> DeletePlayerAsync(int id)
        {

            try
            {
               await PlayerServicer.DeletePlayerAsync(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Player deleted!");
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
