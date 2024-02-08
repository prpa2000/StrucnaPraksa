using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Praksa.WebApi.Controllers
{
    public class Player
    {
        private int id;
        private int footballclubid;
        private string firstname;
        private string lastname;
        private int age;
        private FootballClub footballclub;

        public Player() { }
        public Player(int id, int footballclubid, string firstname, string lastname, int age, FootballClub footballclub)
        {
            this.id = id;
            this.footballclubid = footballclubid;
            this.firstname = firstname;
            this.lastname = lastname;
            this.age = age;
            this.footballclub = footballclub;
        }

        public int Id
        {
            get { return id; }
            set { id = value; } 
        }

        public int FootballClubId
        {
            get { return footballclubid; }
            set { footballclubid = value;}
        }

        public string FirstName
        {
            get { return firstname; }
            set { firstname = value; }
        }

        public string LastName
        {
            get { return lastname; }
            set { lastname = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public FootballClub FootballClub
        {
            get { return footballclub; }
            set { footballclub = value; }
        }
    }
}