
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
        List<FootballClub> GetAllClubs();
        FootballClub GetClubById(int id);
        void CreateFootballClub(FootballClub footballClub);
        void UpdateFootballClub(int id, FootballClub footballClub);
        void DeleteFootballClub(int id);
    }
}
