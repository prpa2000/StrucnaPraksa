using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Praksa.WebApi.Controllers
{
    public class CreatedPlayer
    {
        private int id;
        private int footballclubid;
        private string firstname;
        private string lastname;
        private int age;

        public CreatedPlayer() { }
        public CreatedPlayer(int id, int footballclubid, string firstname, string lastname, int age)
        {
            this.id = id;
            this.footballclubid = footballclubid;
            this.firstname = firstname;
            this.lastname = lastname;
            this.age = age;
           
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int FootballClubId
        {
            get { return footballclubid; }
            set { footballclubid = value; }
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
    }
}