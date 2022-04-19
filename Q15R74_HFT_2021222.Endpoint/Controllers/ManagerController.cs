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
    public class ManagerController : ControllerBase
    {

        IManagerLogic logic;

        public ManagerController(IManagerLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<Manager> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Manager Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Manager value)
        {
            this.logic.Create(value);
        }

        [HttpPut]
        public void Put([FromBody] Manager value)
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
