using HandicapTrackerAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HandicapTrackerAPI.DAL
{
    public class HoleSqlDAO : IHoleDAO
    {
        internal static Hole RowToObject(SqlDataReader rdr)
        {
            Hole hole = new Hole();

            hole.HoleId = Convert.ToInt32(rdr["HoleId"]);
            hole.HoleNumber = Convert.ToInt32(rdr["HoleNumber"]);
            hole.HoleIndex = Convert.ToInt32(rdr["HoleIndex"]);
            hole.ParScore = Convert.ToInt32(rdr["ParScore"]);

            return hole;

        }
    }
}
