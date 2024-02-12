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
using Praksa.Model;
using Praksa.Service.Common;
using System.Threading.Tasks;
namespace Praksa.WebApi.Controllers
{
   
    public class FootballClubController : ApiController
    {

        IFootballClubService FootballClubServicer { get; set; }
        
        public FootballClubController(IFootballClubService footballClubService)
        {
            FootballClubServicer = footballClubService;
        }
        public async Task<List<FootballClub>> GetAllClubsAsync()
        {
            try
            {
                return await FootballClubServicer.GetAllClubsAsync();
            }
            catch
            {
                throw;
            }


        }

        public async Task<FootballClub> GetClubByIdAsync(int id)
        {
            try
            {
                return await FootballClubServicer.GetClubByIdAsync(id);
            }
            catch
            {
                throw;
            }
        }
        
        

        [System.Web.Http.HttpPost]
        public async Task CreateFootballClubAsync (FootballClub footballClub)
        {
            try
            {
                await FootballClubServicer.CreateFootballClubAsync(footballClub);
            }
            catch
            {
                throw;
            }
        }
        



        [System.Web.Http.HttpPut]
        public async Task UpdateFootballClubAsync(int id, FootballClub footballclub)
        {
            try
            {
                await FootballClubServicer.UpdateFootballClubAsync(id, footballclub);
            }
            catch
            {
                throw;
            }
        }



        public async Task DeleteFootballClubAsync(int id)
        {
            try
            {
                await FootballClubServicer.DeleteFootballClubAsync(id);
            }
            catch
            {
                throw;
            }
        }
       
    }
}
