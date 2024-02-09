using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praksa.Model.Common
{
    public interface IPlayerModel
    {
      int Id { get; set; }
      int? FootballClubId { get; set; }
     string FirstName {  get; set; }
        string LastName { get; set; }
        int Age { get; set; }
    }
}
