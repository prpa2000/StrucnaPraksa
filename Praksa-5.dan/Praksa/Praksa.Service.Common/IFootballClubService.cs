
using Praksa.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praksa.Service.Common
{
    public interface IFootballClubService
    {
        Task<List<FootballClub>> GetAllClubsAsync();
        Task<FootballClub> GetClubByIdAsync(int id);
        Task CreateFootballClubAsync(FootballClub footballClub);
        Task UpdateFootballClubAsync(int id, FootballClub footballClub);
        Task DeleteFootballClubAsync(int id);
    }
}
