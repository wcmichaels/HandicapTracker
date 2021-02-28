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
        private const string SQL_CREATE_HOLE_RESULTS = @"INSERT INTO HoleResult (GolfRoundId, HoleId, Score, HitFairway, Putts, InGreensideBunker, OutOfBounds, InWater, DropOrOther) VALUES
                                                          (@golfRoundId, @holeId, @score, @hitFairway, @putts, @inGreensideBunker, @outOfBounds, @inWater, @dropOrOther);";

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

                    rdr.Close();
                   

                    if (round.HoleResults.Count > 0)
                    {
                        CreateHoleResults(conn, round, createdRound.GolfRoundId);


                    }

                    return createdRound;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void CreateHoleResults(SqlConnection conn, GolfRound round, int roundId)
        {
            for (int i = 0; i < 18; i++)
            {

                SqlCommand cmd = new SqlCommand(SQL_CREATE_HOLE_RESULTS, conn);
                cmd.Parameters.AddWithValue("@golfRoundId", roundId);
                cmd.Parameters.AddWithValue("@holeId", round.Tee.Course.Holes[i].HoleId);
                cmd.Parameters.AddWithValue("@score", round.HoleResults[i].Score);
                cmd.Parameters.AddWithValue("@hitFairway", round.HoleResults[i].HitFairway);
                cmd.Parameters.AddWithValue("@putts", round.HoleResults[i].Putts);
                cmd.Parameters.AddWithValue("@inGreensideBunker", round.HoleResults[i].InGreensideBunker);
                cmd.Parameters.AddWithValue("@outOfBounds", round.HoleResults[i].OutOfBounds);
                cmd.Parameters.AddWithValue("@inWater", round.HoleResults[i].InWater);
                cmd.Parameters.AddWithValue("@dropOrOther", round.HoleResults[i].DropOrOther);

                cmd.ExecuteNonQuery();
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
