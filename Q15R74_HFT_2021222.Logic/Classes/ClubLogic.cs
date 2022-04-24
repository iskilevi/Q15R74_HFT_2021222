using System;
using System.Collections.Generic;
using System.Linq;
using Q15R74_HFT_2021222.Models;
using Q15R74_HFT_2021222.Repository;

namespace Q15R74_HFT_2021222.Logic
{
    public class ClubLogic : IClubLogic
    {

        IRepository<Club> repo;

        public ClubLogic(IRepository<Club> repo)
        {
            this.repo = repo;
        }

        public void Create(Club item)
        {
            if (item.Name.Length < 3)
            {
                throw new ArgumentException("Club name is too short...");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Club Read(int id)
        {
            var club = this.repo.Read(id);
            if (club == null)
            {
                throw new ArgumentException("Club does not exist...");
            }
            return club;
        }

        public IQueryable<Club> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Club item)
        {
            this.repo.Update(item);
        }


        // Non- CRUDs


        public IEnumerable<string> NationList()
        {
            return this.repo
               .ReadAll()
               .Select(t => t.Nation)
               .Distinct();
        }

    }
}
