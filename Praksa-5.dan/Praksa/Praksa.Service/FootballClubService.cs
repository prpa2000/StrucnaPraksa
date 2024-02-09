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

        public List<FootballClub> GetAllClubs()
        {
            try
            {
                return footballclubrepository.GetAllClubs();
            }
            catch 
            {
               
                throw;
            }
        }

        public FootballClub GetClubById(int id)
        {
            try
            {
                return footballclubrepository.GetClubById(id);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void CreateFootballClub(FootballClub footballClub)
        {
            footballclubrepository.CreateFootballClub(footballClub);
        }
        
        public void UpdateFootballClub(int id, FootballClub footballClub)
        {
            try
            {
                footballclubrepository.UpdateFootballClub(id, footballClub);
            }
            catch
            {
                throw;
            }
        }

        public void DeleteFootballClub(int id)
        {
            try
            {
                footballclubrepository.DeleteFootballClub(id);
            }
            catch
            {
                throw;
            }

        }

    }
}
