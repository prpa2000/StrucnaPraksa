using Praksa.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praksa.Repository;

using Praksa.Service.Common;
using Praksa.Model;
using System.Net.Http;
using System.Net;
using Praksa.Common;
namespace Praksa.Service
{
    public class FootballClubService : IFootballClubService
    {

        public IFootballClubRepository FootballClubRepository { get; }

        public FootballClubService(IFootballClubRepository footballClubRepository)
        {
            FootballClubRepository = footballClubRepository;
        }

        public async Task<List<FootballClub>> GetAllClubsAsync(Paging paging, Sorting sorting, FootballClubFiltering filters)
        {
            try
            {
               return await FootballClubRepository.GetAllClubsAsync(paging, sorting, filters);
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
                return await FootballClubRepository.GetClubByIdAsync(id);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task CreateFootballClubAsync(FootballClub footballClub)
        {
            try
            {
               await FootballClubRepository.CreateFootballClubAsync(footballClub);
                

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task UpdateFootballClubAsync(int id, FootballClub footballClub)
        {
            try
            {
                await FootballClubRepository.UpdateFootballClubAsync(id, footballClub);
               
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteFootballClubAsync(int id)
        {
            try
            {
              await FootballClubRepository.DeleteFootballClubAsync(id);
               
            }
            catch(Exception ex) 
            {
                throw ex;
            }

        }

    }
}
