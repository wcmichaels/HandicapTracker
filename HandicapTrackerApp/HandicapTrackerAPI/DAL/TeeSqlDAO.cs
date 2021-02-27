using HandicapTrackerAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HandicapTrackerAPI.DAL
{
    public class TeeSqlDAO : ITeeDAO
    {
        internal static Tee RowToObject(SqlDataReader rdr)
        {
            Tee tee = new Tee();

            tee.TeeId = Convert.ToInt32(rdr["TeeId"]);
            tee.Name = Convert.ToString(rdr["Name"]);
            tee.RatingFull = Convert.ToDouble(rdr["RatingFull"]);
            tee.SlopeFull = Convert.ToInt32(rdr["SlopeFull"]);
            tee.RatingFront = Convert.ToDouble(rdr["RatingFront"]);
            tee.SlopeFront = Convert.ToInt32(rdr["SlopeFront"]);
            tee.RatingBack = Convert.ToDouble(rdr["RatingBack"]);
            tee.SlopeBack = Convert.ToInt32(rdr["SlopeBack"]);
            tee.Yardage = Convert.ToInt32(rdr["Yardage"]);
           
            tee.Course = null;

            return tee;
        }
    }
}
