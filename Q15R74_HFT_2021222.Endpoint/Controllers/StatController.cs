using Microsoft.AspNetCore.Mvc;
using Q15R74_HFT_2021222.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Q15R74_HFT_2021222.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IPlayerLogic logic;

        public StatController(IPlayerLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<PlayerLogic.ClubAvgAgeInfo> ClubAvgAge()
        {
            return this.logic.ClubAvgAge();
        }

        [HttpGet]
        public PlayerLogic.HighestPaidClubInfo HighestPaidClub()
        {
            return this.logic.HighestPaidClub();
        }

        [HttpGet]
        public PlayerLogic.BestManagerInfo BestManager()
        {
            return this.logic.BestManager();
        }
    }
}
