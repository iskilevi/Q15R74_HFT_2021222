using Q15R74_HFT_2021222.Models;
using Q15R74_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q15R74_HFT_2021222.Logic
{
    public class ManagerLogic : IManagerLogic
    {
        IRepository<Manager> repo;

        public ManagerLogic(IRepository<Manager> repo)
        {
            this.repo = repo;
        }

        public void Create(Manager item)
        {
            if (item.Name.Length < 3)
            {
                throw new ArgumentException("Manager name is too short...");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Manager Read(int id)
        {
            var man = this.repo.Read(id);
            if (man == null)
            {
                throw new ArgumentException("Manager does not exist...");
            }
            return man;
        }

        public IQueryable<Manager> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Manager item)
        {
            this.repo.Update(item);
        }


        //Non- CRUDs


    }
}
