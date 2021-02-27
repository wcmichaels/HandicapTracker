﻿using HandicapTrackerAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HandicapTrackerAPI.DAL
{
    public class PlayerSqlDAO : IPlayerDAO
    {
        private readonly string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=GolfAppDB;Trusted_Connection=true;";
        private const string SQL_GET_PLAYER_BY_ID = "SELECT * FROM PLAYER WHERE PlayerId = @playerId;";
        private const string SQL_LIST_PLAYERS = "SELECT * FROM Player";
        private const string SQL_CREATE_PLAYER = @"INSERT INTO PLAYER
                                                    (FirstName, LastName, UserName, Password, DOB, StreetAddress, City, State,
                                                     CountryCode, PostalCode, Email, Phone)
                                                     Values (@firstName, @lastName, @username, @password, @dob, @streetAddress, @city, @state,
                                                     @countryCode, @postalCode, @email, @phone); SELECT * FROM Player WHERE PlayerId = @@IDENTITY;";
        private const string SQL_UPDATE_PLAYER = @"UPDATE PLAYER SET FirstName = @firstName, LastName = @lastName, UserName = @username, Password = @password,
                                                 DOB = @dob, StreetAddress = @streetAddress, City = @city, State = @state, CountryCode = @countryCode,
                                                 PostalCode = @postalCode, Email = @email, Phone = @phone WHERE PlayerId = @playerId;
                                                 SELECT * FROM Player WHERE PlayerId = @playerId";

        public Player GetPlayerById(int playerId)
        {

            Player player = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GET_PLAYER_BY_ID, conn);
                    cmd.Parameters.AddWithValue("@playerId", playerId);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        player = RowToObject(rdr);
                    }

                    return player;
                }

            }
            catch (SqlException ex)
            {

                throw;
            }
        }

        public List<Player> ListPlayers()
        {
            List<Player> players = new List<Player>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_LIST_PLAYERS, conn);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Player player = RowToObject(rdr);
                        players.Add(player);
                    }

                }

                return players;
            }
            catch (SqlException ex)
            {

                throw;
            }
        }

        public Player CreatePlayer(Player player)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_CREATE_PLAYER, conn);
                    AddPlayerParameters(player, cmd);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        player = RowToObject(rdr);
                    }
                    else
                    {
                        player = null;
                    }

                    return player;

                }
            }
            catch (SqlException ex)
            {

                throw;
            }
        }

        private static void AddPlayerParameters(Player player, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@playerId", player.PlayerId);
            cmd.Parameters.AddWithValue("@firstName", player.FirstName);
            cmd.Parameters.AddWithValue("@lastName", player.LastName);
            cmd.Parameters.AddWithValue("@username", player.Username);
            cmd.Parameters.AddWithValue("@password", player.Password);
            cmd.Parameters.AddWithValue("@dob", player.DOB);
            cmd.Parameters.AddWithValue("@streetAddress", player.StreetAddress);
            cmd.Parameters.AddWithValue("@city", player.City);
            cmd.Parameters.AddWithValue("@state", player.State);
            cmd.Parameters.AddWithValue("@countryCode", player.CountryCode);
            cmd.Parameters.AddWithValue("@postalCode", player.PostalCode);
            cmd.Parameters.AddWithValue("@email", player.Email);
            cmd.Parameters.AddWithValue("@phone", player.Phone);
        }

        public Player UpdatePlayer(Player player)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_UPDATE_PLAYER, conn);
                    AddPlayerParameters(player, cmd);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        player = RowToObject(rdr);
                        return player;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        private Player RowToObject(SqlDataReader rdr)
        {
            Player player = new Player();

            player.PlayerId = Convert.ToInt32(rdr["PlayerId"]);
            player.FirstName = Convert.ToString(rdr["FirstName"]);
            player.LastName = Convert.ToString(rdr["LastName"]);
            player.Username = Convert.ToString(rdr["Username"]);
            player.Password = Convert.ToString(rdr["Password"]);
            player.DOB = Convert.ToDateTime(rdr["DOB"]);
            player.StreetAddress = Convert.ToString(rdr["StreetAddress"]);
            player.City = Convert.ToString(rdr["City"]);
            player.State = Convert.ToString(rdr["State"]);
            player.CountryCode = Convert.ToString(rdr["CountryCode"]);
            player.PostalCode = Convert.ToString(rdr["PostalCode"]);
            player.Email = Convert.ToString(rdr["Email"]);
            player.Phone = Convert.ToString(rdr["Phone"]);

            return player;
        }
    }
}
