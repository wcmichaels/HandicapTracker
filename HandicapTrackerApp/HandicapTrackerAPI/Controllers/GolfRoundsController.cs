using HandicapTrackerAPI.DAL;
using HandicapTrackerAPI.Models;
using HandicapTrackerAPI.Util;
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
        private IPlayerDAO playerDAO;

        public GolfRoundsController(IGolfRoundDAO golfRoundDAO, IPlayerDAO playerDAO)
        {
            this.golfRoundDAO = golfRoundDAO;
            this.playerDAO = playerDAO;
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

            if (createdRound == null)
            {
                return BadRequest();
            }

            List<GolfRound> rounds = golfRoundDAO.GetGolfRoundsByPlayerId(createdRound.PlayerId);
            List<double> scoreDifferentials = HandicapCalculator.CalculateScoreDifferentials(rounds);
            double handicap = HandicapCalculator.CalculateHandicap(scoreDifferentials);
            playerDAO.UpdateHandicap(handicap, createdRound.PlayerId);

            return CreatedAtRoute("round", new { id = createdRound.GolfRoundId }, createdRound);


        }

    }
}
