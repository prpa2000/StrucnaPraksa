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
using Praksa.Common;

namespace Praksa.WebApi.Controllers
{
   
    public class FootballClubController : ApiController
    {

        IFootballClubService FootballClubServicer { get; set; }
        
        public FootballClubController(IFootballClubService footballClubService)
        {
            FootballClubServicer = footballClubService;
        }
        public async Task<List<FootballClub>> GetAllClubsAsync(int? pagenumber = null, int? pagesize = null, string sortby = "", string sortorder = "", int? id = null, string name = "", int? numberoftrophies = null)
        {
            try
            {
                Paging paging = new Paging(pagenumber, pagesize);
                Sorting sorting = new Sorting(sortby, sortorder);
                FootballClubFiltering filters = new FootballClubFiltering(id, name, numberoftrophies);


                return await FootballClubServicer.GetAllClubsAsync(paging, sorting, filters);
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
        public async Task<HttpResponseMessage> CreateFootballClubAsync (FootballClub footballClub)
        {
            if(footballClub == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Football club cannot be null object!");
            }
            try
            {
                await FootballClubServicer.CreateFootballClubAsync(footballClub);
                return Request.CreateResponse(HttpStatusCode.OK, "Football club created!");
               
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        



        [System.Web.Http.HttpPut]
        public async Task<HttpResponseMessage> UpdateFootballClubAsync(int id, FootballClub footballclub)
        {
            if(footballclub == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Football club cannot be null object");
            }
            try
            {
                 await FootballClubServicer.UpdateFootballClubAsync(id, footballclub);
                return Request.CreateResponse(HttpStatusCode.OK, "Football club updated!");
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        public async Task<HttpResponseMessage> DeleteFootballClubAsync(int id)
        {
            try
            {
                await FootballClubServicer.DeleteFootballClubAsync(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Football club deleted!");
                
            }
            catch(Exception e) 
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }
       
    }
}
