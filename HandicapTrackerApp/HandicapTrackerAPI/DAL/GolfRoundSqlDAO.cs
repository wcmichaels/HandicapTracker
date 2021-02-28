using HandicapTrackerAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HandicapTrackerAPI.DAL
{
    public class GolfRoundSqlDAO : IGolfRoundDAO
    {

        private const string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=GolfAppDB;Trusted_Connection=true;";
        private const string SQL_GET_GOLF_ROUNDS_BY_PLAYERID = @"SELECT * FROM GolfRound gr
                                                                JOIN Tee t ON t.TeeId = gr.TeeId
                                                                JOIN Course c ON c.CourseId = t.CourseId
                                                                WHERE PlayerId = @playerId;";
        private const string SQL_CREATE_GOLF_ROUND = @"INSERT INTO GolfRound (PlayerId, TeeId, DatePlayed, Score) VALUES
                                                        (@playerId, @teeId, @datePlayed, @score);
                                                     SELECT * FROM GolfRound gr
                                                        JOIN Tee t ON t.TeeId = gr.TeeId
                                                        JOIN Course c ON c.CourseId = t.CourseId
                                                        WHERE gr.GolfRoundId = @@IDENTITY;";
        private const string SQL_GET_GOLF_ROUND_BY_ID = "SELECT * FROM GolfRound WHERE GolfRoundId = @golfRoundId";
        private const string SQL_UPDATE_HANDICAP = "UPDATE Player SET Handicap = @handicap WHERE PlayerId = @playerId;";

        public List<GolfRound> GetGolfRoundsByPlayerId(int playerId)
        {
            List<GolfRound> golfRounds = new List<GolfRound>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GET_GOLF_ROUNDS_BY_PLAYERID, conn);
                    cmd.Parameters.AddWithValue("@playerId", playerId);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        GolfRound golfRound = RowToObject(rdr);
                        Course course = CourseSqlDAO.RowToObject(rdr);
                        Tee tee = TeeSqlDAO.RowToObject(rdr);
                        tee.Course = course;
                        golfRound.Tee = tee;
                        golfRounds.Add(golfRound);
                    }

                }

                return golfRounds;
            }
            catch (SqlException ex)
            {

                throw;
            }
        }
        public GolfRound GetGolfRoundById(int id)
        {
            GolfRound round = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GET_GOLF_ROUND_BY_ID, conn);
                    cmd.Parameters.AddWithValue("@golfRoundId", id);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        round = RowToObject(rdr);
                    }

                    return round;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public GolfRound CreateGolfRound(GolfRound round)
        {
            GolfRound createdRound = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_CREATE_GOLF_ROUND, conn);
                    cmd.Parameters.AddWithValue("@playerId", round.PlayerId);
                    cmd.Parameters.AddWithValue("@teeId", round.Tee.TeeId);
                    cmd.Parameters.AddWithValue("@datePlayed", round.DatePlayed);
                    cmd.Parameters.AddWithValue("@score", round.Score);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        Tee tee = TeeSqlDAO.RowToObject(rdr);
                        Course course = CourseSqlDAO.RowToObject(rdr);
                        createdRound = RowToObject(rdr);
                        tee.Course = course;
                        createdRound.Tee = tee;

                    }

                    return createdRound;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        internal static GolfRound RowToObject(SqlDataReader rdr)
        {
            GolfRound round = new GolfRound();

            round.GolfRoundId = Convert.ToInt32(rdr["GolfRoundId"]);
            round.PlayerId = Convert.ToInt32(rdr["PlayerId"]);
            //round.TeeId = Convert.ToInt32(rdr["TeeId"]);
            round.DatePlayed = Convert.ToDateTime(rdr["DatePlayed"]);
            round.Score = Convert.ToInt32(rdr["Score"]);

            return round;
        }
    }
}
