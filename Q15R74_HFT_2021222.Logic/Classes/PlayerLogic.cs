using Q15R74_HFT_2021222.Models;
using Q15R74_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q15R74_HFT_2021222.Logic
{
    public class PlayerLogic : IPlayerLogic
    {

        IRepository<Player> repo;

        public PlayerLogic(IRepository<Player> repo)
        {
            this.repo = repo;
        }

        public void Create(Player item)
        {
            if (item.Name.Length < 3)
            {
                throw new ArgumentException("Player name is too short...");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Player Read(int id)
        {
            var player = this.repo.Read(id);
            if (player == null)
            {
                throw new ArgumentException("Player does not exist...");
            }
            return player;
        }

        public IQueryable<Player> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Player item)
        {
            this.repo.Update(item);
        }

        //NON-CRUDs


        //How much is the average age in a team??
        public IEnumerable<ClubAvgAgeInfo> ClubAvgAge()
        {
            return from x in this.repo.ReadAll()
                   group x by x.Club.Name into g
                   select new ClubAvgAgeInfo()
                   {
                       ClubName = g.Key,
                       AvgAge = g.Average(t => t.Age)
                   };
        }

        public class ClubAvgAgeInfo
        {
            public string ClubName { get; set; }
            public double? AvgAge { get; set; }
        }

        //Which team players got the highest salary summarized ??
        public IEnumerable<HighestPaidClubInfo> HighestPaidClub()
        {
            return (from x in this.repo.ReadAll()
                   group x by x.Club.Name into g
                   select new HighestPaidClubInfo()
                   {
                       ClubName = g.Key,
                       SalarySum = g.Sum(t => t.Salary)
                   }).OrderByDescending(t => t.SalarySum).Take(1);
        }

        public class HighestPaidClubInfo
        {
            public string ClubName { get; set; }
            public double? SalarySum { get; set; }
        }

    }
}
