using Microsoft.AspNetCore.Mvc;
using Q15R74_HFT_2021222.Logic;
using Q15R74_HFT_2021222.Models;
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
        IPlayerLogic pLogic;

        public StatController(IPlayerLogic logic)
        {
            this.pLogic = logic;
        }

        [HttpGet]
        public IEnumerable<PlayerLogic.ClubAvgAgeInfo> ClubAvgAge()
        {
            return this.pLogic.ClubAvgAge();
        }

        [HttpGet]
        public PlayerLogic.HighestPaidClubInfo HighestPaidClub()
        {
            return this.pLogic.HighestPaidClub();
        }

        [HttpGet]
        public PlayerLogic.BestManagerInfo BestManager()
        {
            return this.pLogic.BestManager();
        }

        [HttpGet("{clubId}")]
        public IEnumerable<string> PlayerList(int clubId)
        {
            return this.pLogic.PlayerList(clubId);
        }

        [HttpGet]
        public PlayerLogic.BestAttackerInfo BestAttacker()
        {
            return this.pLogic.BestAttacker();
        }
    }
}
