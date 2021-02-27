using HandicapTrackerAPI.DAL;
using HandicapTrackerAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandicapTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerDAO playerDAO;
        public PlayersController(IPlayerDAO playerDAO)
        {
            this.playerDAO = playerDAO;
        }

        [HttpGet]
        public ActionResult<List<Player>> ListPlayers()
        {
            List<Player> players = playerDAO.ListPlayers();

            return players;
        }

        [HttpGet("login")]
        public ActionResult<Player> SearchByUsernamePassword(string username, string password)
        {
            Player player = playerDAO.GetPlayerByUsernamePassword(username, password);

            if (player != null)
            {
                return Ok(player);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id}", Name = "player")]
        public ActionResult<Player> GetPlayerById(int id)
        {
            Player player = playerDAO.GetPlayerById(id);

            if (player != null)
            {
                return Ok(player);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult<Player> CreatePlayer(Player player)
        {

            Player createdPlayer = playerDAO.CreatePlayer(player);

            return CreatedAtRoute("player", new { id = createdPlayer.PlayerId }, createdPlayer);

        }

        [HttpPut("{id}")]
        public ActionResult<Player> UpdatePlayer(Player player, int id)
        {
            if (id != player.PlayerId)
            {
                return BadRequest();
            }

            Player updatedPlayer = playerDAO.UpdatePlayer(player);

            if (updatedPlayer != null)
            {
                return Ok(updatedPlayer);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
