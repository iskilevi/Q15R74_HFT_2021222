using Microsoft.AspNetCore.Mvc;
using Q15R74_HFT_2021222.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Q15R74_HFT_2021222.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ClubStatController : ControllerBase
    {
        IClubLogic cLogic;

        public ClubStatController(IClubLogic logic)
        {
            cLogic = logic;
        }

        [HttpGet]
        public IEnumerable<string> NationList()
        {
            return this.cLogic.NationList();
        }
    }
}
