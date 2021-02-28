using HandicapTrackerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandicapTrackerAPI.DAL
{
    public interface ITeeDAO
    {
        public List<Tee> GetTees();
        Tee GetTeeWithHolesById(int teeId);
    }
}
