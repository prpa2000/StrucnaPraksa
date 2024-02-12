using Praksa.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praksa.Repository;

using Praksa.Service.Common;
using Praksa.Model;
namespace Praksa.Service
{
    public class FootballClubService : IFootballClubService
    {

        IFootballClubRepository FootballClubRepository { get; set; }
        public FootballClubService(IFootballClubRepository footballclubrepository)
        {
            FootballClubRepository = footballclubrepository;
        }

        public async Task<List<FootballClub>> GetAllClubsAsync()
        {
            try
            {
                return await FootballClubRepository.GetAllClubsAsync();
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
            await FootballClubRepository.CreateFootballClubAsync(footballClub);
        }
        
        public async Task UpdateFootballClubAsync(int id, FootballClub footballClub)
        {
            try
            {
                await FootballClubRepository.UpdateFootballClubAsync(id, footballClub);
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
                await FootballClubRepository.DeleteFootballClubAsync(id);
            }
            catch
            {
                throw;
            }

        }

    }
}
