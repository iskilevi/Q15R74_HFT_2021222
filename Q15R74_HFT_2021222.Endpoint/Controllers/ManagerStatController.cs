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
    public class ManagerStatController : ControllerBase
    {
        IManagerLogic mLogic;

        public ManagerStatController(IManagerLogic logic)
        {
            mLogic = logic;
        }

        [HttpGet]
        public double? ManagerAvgSal()
        {
            return this.mLogic.ManagerAvgSal();
        }
    }
}
