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

        IFootballClubRepository footballclubrepository;
        public FootballClubService(IFootballClubRepository footballclubrepository)
        {
            this.footballclubrepository = footballclubrepository;
        }

        public async Task<List<FootballClub>> GetAllClubsAsync()
        {
            try
            {
                return await footballclubrepository.GetAllClubsAsync();
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
                return await footballclubrepository.GetClubByIdAsync(id);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task CreateFootballClubAsync(FootballClub footballClub)
        {
            await footballclubrepository.CreateFootballClubAsync(footballClub);
        }
        
        public async Task UpdateFootballClubAsync(int id, FootballClub footballClub)
        {
            try
            {
                await footballclubrepository.UpdateFootballClubAsync(id, footballClub);
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
                await footballclubrepository.DeleteFootballClubAsync(id);
            }
            catch
            {
                throw;
            }

        }

    }
}
