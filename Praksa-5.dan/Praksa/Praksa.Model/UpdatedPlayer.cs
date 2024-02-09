using Praksa.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Praksa.Model
{
    public class UpdatedPlayer : IUpdatedPlayerModel
    {
        private int footballclubid;
        private string firstname;
        private string lastname;
        private int age;

        public UpdatedPlayer(int footballclubid, string firstname, string lastname, int age)
        {
            this.footballclubid = footballclubid;
            this.firstname = firstname;
            this.lastname = lastname;
            this.age = age;
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
    }
}