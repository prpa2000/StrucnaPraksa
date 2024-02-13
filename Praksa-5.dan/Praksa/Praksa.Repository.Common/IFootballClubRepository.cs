using Praksa.Common;
using Praksa.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Praksa.Repository.Common
{
    public interface IFootballClubRepository
    {
        Task<List<FootballClub>> GetAllClubsAsync(Paging paging, Sorting sorting, FootballClubFiltering filters);
        Task<FootballClub> GetClubByIdAsync(int id);
        Task<HttpResponseMessage> CreateFootballClubAsync(FootballClub footballclub);
        Task<HttpResponseMessage> UpdateFootballClubAsync(int id,  FootballClub footballclub);
        Task<HttpResponseMessage> DeleteFootballClubAsync(int id);
    }
}
