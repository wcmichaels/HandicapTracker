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
        private const string SQL_GET_GOLF_ROUNDS_BY_PLAYERID = "SELECT * FROM GolfRound WHERE PlayerId = @playerId;";
        private const string SQL_CREATE_GOLF_ROUND = @"INSERT INTO GolfRound (PlayerId, TeeId, DatePlayed, Score) VALUES
                                                        (@playerId, @teeId, @datePlayed, @score); SELECT @@IDENTITY;";

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
                        createdRound = RowToObject(rdr);
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
