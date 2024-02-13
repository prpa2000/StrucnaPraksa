using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praksa.Common
{
    public class FootballClubFiltering
    {
        int? id;
        string name;
        int? numberoftrophies;

        public FootballClubFiltering(int? id, string name, int? numberoftrophies)
        {
            this.id = id;
            this.name = name;
            this.numberoftrophies = numberoftrophies;
        }

        public int? Id
        {
               get { return id; }
               set { id = value; }
        }
        public string Name { get { return name; } set {  name = value; } }
        public int? NumberOfTrophies { get {  return numberoftrophies; } set {  numberoftrophies = value; } }
    }
}
