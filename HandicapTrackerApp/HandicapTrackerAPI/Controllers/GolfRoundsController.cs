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

        [HttpPost("{playerId}")]
        public ActionResult<GolfRound> CreateGolfRound(int playerId, GolfRound round)
        {
            if (playerId != round.PlayerId)
            {
                return BadRequest();
            }

            GolfRound createdRound = golfRoundDAO.CreateGolfRound(round);
        }

    }
}
