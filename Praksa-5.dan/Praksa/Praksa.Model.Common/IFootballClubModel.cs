using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praksa.Model.Common
{
    public interface IFootballClubModel
    {
        int Id { get; set; }
        string Name { get; set; }


        int NumberOfTrophies { get; set; }
    }
}
