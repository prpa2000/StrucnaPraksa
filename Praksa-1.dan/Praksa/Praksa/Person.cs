using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praksa
{
    public abstract class Person
    {
        private string firstname;
        private string lastname;
        private string jmbg;
        private string address;

        public Person() { firstname = lastname = jmbg = address = ""; }

        public Person(string firstname, string lastname, string jmbg, string address)
        {
            this.firstname = firstname;
            this.lastname = lastname;
            this.jmbg = jmbg;
            this.address = address;
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

        public string Jmbg
        {
            get { return jmbg; }
            set { jmbg = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public abstract string GetPersonDescription();
       
    }
}
