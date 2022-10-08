using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Q15R74_HFT_2021222.Endpoint.Services;
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
        IHubContext<SignalRHub> hub;

        public ManagerController(IManagerLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("ManagerCreated", value);
        }

        [HttpPut]
        public void Put([FromBody] Manager value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("ManagerUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var managerToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("ManagerDeleted", managerToDelete);
        }
    }
}
