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
    public class GolfRoundsController : ControllerBase
    {
        private IGolfRoundDAO golfRoundDAO;

        public GolfRoundsController(IGolfRoundDAO golfRoundDAO)
        {
            this.golfRoundDAO = golfRoundDAO;
        }

        [HttpGet("{id}", Name = "round")]
        public ActionResult<GolfRound> GetGolfRoundById(int id)
        {
            GolfRound round = golfRoundDAO.GetGolfRoundById(id);

            if (round != null)
            {
                return Ok(round);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult<GolfRound> CreateGolfRound(GolfRound round)
        {
            GolfRound createdRound = golfRoundDAO.CreateGolfRound(round);
            // Make call to playerdAO for handicap calculation
            // Return handicap with an OK 200
            /* 
             * use playerId from createdRound to do another call to DB to get rounds
             * for that playerId.  Then make pass that list of rounds into util method to 
             * calculate handicap, then add handicap into db and return to front end
             * in util the class should be a static class
             */

            if (createdRound != null)
            {
                return CreatedAtRoute("round", new { id = createdRound.GolfRoundId }, createdRound);
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
