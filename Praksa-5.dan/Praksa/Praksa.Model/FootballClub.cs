using Praksa.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praksa.Model
{
    public class FootballClub : IFootballClubModel
    {
        private int id;
        private string name;
        private int numberoftrophies;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int NumberOfTrophies
        {
            get { return numberoftrophies; }
            set { numberoftrophies = value; }
        }
    }
}
