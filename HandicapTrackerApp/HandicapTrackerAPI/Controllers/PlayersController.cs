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
    [Route("[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerDAO dao;
        public PlayersController(IPlayerDAO playerDAO)
        {
            dao = playerDAO;
        }

        [HttpGet]
        public ActionResult<List<Player>> ListPlayers()
        {
            List<Player> players = dao.ListPlayers();

            return players;
        }

        [HttpGet("{id}", Name = "player")]
        public ActionResult<Player> GetPlayerById(int id)
        {
            Player player = dao.GetPlayerById(id);

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

            Player createdPlayer = dao.CreatePlayer(player);

            return CreatedAtRoute("player", new { id = createdPlayer.PlayerId }, createdPlayer);

        }

        [HttpPut("{id}")]
        public ActionResult<Player> UpdatePlayer(Player player, int id)
        {
            if (id != player.PlayerId)
            {
                return BadRequest();
            }

            Player updatedPlayer = dao.UpdatePlayer(player);

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
