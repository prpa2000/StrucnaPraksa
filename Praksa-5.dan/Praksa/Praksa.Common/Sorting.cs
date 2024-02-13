using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praksa.Common
{
    public class Sorting
    {
        string sortby;
        string sortorder;
        public Sorting(string sortby, string sortorder)
        {
            this.sortby = sortby;
            this.sortorder = sortorder;
        }

        public string SortBy { get {  return sortby; } set {  sortby = value; } }
        public string SortOrder { get {  return sortorder; } set { sortorder = value; } }
    }
}
