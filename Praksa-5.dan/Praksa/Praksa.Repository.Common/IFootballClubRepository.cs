using Praksa.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praksa.Repository.Common
{
    public interface IFootballClubRepository
    {
        Task<List<FootballClub>> GetAllClubsAsync();
        Task<FootballClub> GetClubByIdAsync(int id);
        Task CreateFootballClubAsync(FootballClub footballclub);
        Task UpdateFootballClubAsync(int id,  FootballClub footballclub);
        Task DeleteFootballClubAsync(int id);
    }
}
