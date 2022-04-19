using Microsoft.AspNetCore.Mvc;
using Q15R74_HFT_2021222.Logic;
using Q15R74_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Q15R74_HFT_2021222.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClubController : ControllerBase
    {

        IClubLogic logic;

        public ClubController(IClubLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<Club> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Club Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Club value)
        {
            this.logic.Create(value);
        }

        [HttpPut]
        public void Put([FromBody] Club value)
        {
            this.logic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
