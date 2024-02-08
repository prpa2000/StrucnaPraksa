using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Praksa.WebApi.Controllers
{
    public class FootballClub
    {
        private int id;
        private string name;
        private int numberoftrophies;
        private List<Player> players = new List<Player>();
        public FootballClub() { }
        public FootballClub(int id, string name, int numberoftrophies)
        {
            this.id = id;
            this.name = name;
            this.numberoftrophies = numberoftrophies;
        }
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