using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praksa
{
    public class Student : Person
    {
        private int age;

        public Student() { }
        public Student(string firstname, string lastname, string jmbg, string address, int age) : base (firstname, lastname, jmbg, address)
        {
            this.age = age;
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public override string GetPersonDescription()
        {
            return $"Student: {FirstName} {LastName}, Age: {age}, JMBG: {Jmbg}, Address: {Address}";
        }
    }
}
