using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praksa.Common
{
    public class Paging
    {
        int? pagenumber;
        int? pagesize;

        public Paging(int? pagenumber, int? pagesize)
        {
            this.pagenumber = pagenumber;
            this.pagesize = pagesize;
        }

        public int? PageNumber { get { return pagenumber; } set { pagenumber = value; } }
        public int? PageSize { get {  return pagenumber; } set { pagesize = value; } }
    }
}
