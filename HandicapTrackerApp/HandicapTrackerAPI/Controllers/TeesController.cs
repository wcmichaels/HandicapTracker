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
    public class TeesController : ControllerBase
    {
        
        private readonly ITeeDAO teeDAO;

        public TeesController(ITeeDAO teeDAO)
        {
            this.teeDAO = teeDAO;
        }

        [HttpGet]
        public ActionResult<List<Tee>> GetTees()
        {
            List<Tee> tees = teeDAO.GetTees();

            return Ok(tees);
        }

    }
}
