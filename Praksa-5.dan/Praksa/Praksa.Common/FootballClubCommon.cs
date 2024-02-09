using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praksa.Common
{
    public class FootballClubCommon
    {
        const string connectionString = "Server=127.0.0.1;Port=5432;Database=FootballClub;User Id=postgres;Password=nikolaprpic;";
        public string ConnectionString { get { return connectionString;} }
    }
}
