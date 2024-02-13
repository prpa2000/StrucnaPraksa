using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praksa.Common
{
    public class PlayerFiltering
    {
        int? id;
        string firstname;
        string lastname;
        int? age;
        int? footballclubid;

        public PlayerFiltering(int? id, string firstname, string lastname, int? age, int? footballclubid)
        {
            this.id = id;
            this.firstname = firstname;
            this.lastname = lastname;
            this.age = age;
            this.footballclubid = footballclubid;
        }

        public int? Id { 
            
           get { return id; }
            set { id = value; }
        }

        public string FirstName { get { return firstname; } set { firstname = value; } }
        public string LastName { get { return lastname; } set {  lastname = value; } }
        public int? Age { get { return age; } set { age = value; } }
        public int? FootballClubId {  get { return footballclubid; }set { footballclubid = value; } }
    }
}
