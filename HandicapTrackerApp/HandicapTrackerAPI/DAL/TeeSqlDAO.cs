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
        private readonly string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=GolfAppDB;Trusted_Connection=true;";
        private const string SQL_GET_TEES = "SELECT * FROM Tee t JOIN Course c ON c.CourseId = t.CourseId;";
        private const string SQL_GET_TEE_WITH_HOLES_BY_ID = @"SELECT * FROM Tee t
                                                                JOIN Course c ON c.CourseId = t.CourseId
                                                                WHERE t.TeeId = @teeId;
                                                            SELECT h.* FROM Hole h
                                                                JOIN Course c ON c.CourseId = h.CourseId
                                                                JOIN Tee t ON t.CourseId = c.CourseId
                                                                WHERE t.TeeId = @teeId
                                                                ORDER BY h.HoleNumber;";


        public List<Tee> GetTees()
        {
            List<Tee> tees = new List<Tee>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GET_TEES, conn);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Tee tee = RowToObject(rdr);
                        Course course = CourseSqlDAO.RowToObject(rdr);
                        tee.Course = course;

                        tees.Add(tee);
                    }
                }

                return tees;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Tee GetTeeWithHolesById(int teeId)
        {
            Tee tee = null;
            Course course = null ;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GET_TEE_WITH_HOLES_BY_ID, conn);
                    cmd.Parameters.AddWithValue("@teeId", teeId);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        tee = TeeSqlDAO.RowToObject(rdr);
                        course = CourseSqlDAO.RowToObject(rdr);
                        tee.Course = course;
                    }

                    if (rdr.NextResult())
                    {
                        while (rdr.Read())
                        {
                            Hole hole = HoleSqlDAO.RowToObject(rdr);
                            //hole.Course = course;
                            course.Holes.Add(hole);

                        }
                    }
                }

                return tee;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


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
