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
        List<FootballClub> GetAllClubs();
        FootballClub GetClubById(int id);
        void CreateFootballClub(FootballClub footballclub);
        void UpdateFootballClub(int id,  FootballClub footballclub);
        void DeleteFootballClub(int id);
    }
}
