using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praksa
{
    public class Professor : Person
    {
        private string professortitle;

        public Professor() { professortitle = ""; }

        public Professor(string firstname, string lastname, string jmbg, string address, string professortitle) : base(firstname,lastname, jmbg, address)
        {
            this.professortitle = professortitle;
        }

        public string ProfessorTitle
        {
            get { return professortitle; }
            set { professortitle = value; }
        }

        public override string GetPersonDescription()
        {
            return $"Professor: {FirstName} {LastName}, Title: {professortitle}, JMBG: {Jmbg}, Address: {Address}";
        }
    }
}
