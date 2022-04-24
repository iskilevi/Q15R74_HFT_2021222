using Q15R74_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q15R74_HFT_2021222.Client
{
    class NonCrudService
    {
        private RestService rest;

        public NonCrudService(RestService rest)
        {
            this.rest = rest;
        }

        public void ClubAvgAge()
        {
            List<ClubAvgAgeInfo> items = rest.Get<ClubAvgAgeInfo>("/PlayerStat/ClubAvgAge");
            Console.WriteLine("Name" + "\t\t\t" + "Average Age");
            foreach (var item in items)
            {
                Console.WriteLine(item.ClubName + "\t\t\t" + Math.Round(item.AvgAge.Value, 1));
            }

            Console.ReadLine();
        }

        public void ClubAllGoals()
        {
            var items = rest.Get<ClubAllGoalsInfo>("/PlayerStat/ClubAllGoals");
            Console.WriteLine("Club ID" + "\t\t" + "All Goals (Season)");
            foreach (var item in items)
            {
                Console.WriteLine(item.ClubId + "\t\t" + item.AllGoals);
            }

            Console.ReadLine();
        }

        public void HighestPaid()
        {
            var item = rest.GetSingle<HighestPaidClubInfo>("/PlayerStat/HighestPaidClub");
            Console.WriteLine("Name" + "\t\t\t" + "Salary SUM(M USD/ Year)");

            Console.WriteLine(item.ClubName + "\t\t\t" + item.SalarySum);

            Console.ReadLine();
        }

        public void BestManager()
        {
            var item = rest.GetSingle<BestManagerInfo>("/PlayerStat/BestManager");
            Console.WriteLine("Manager Name" + "\t\t\t" + "Club ID" + "\t\t\t" + "All Goals scored by his team");

            Console.WriteLine(item.ManagerName + "\t\t\t" + item.ClubId + "\t\t\t" + item.AllGoal);

            Console.ReadLine();
        }

        public void ClubPlayers()
        {
            Console.WriteLine("Enter club ID:...");
            int clubId = int.Parse(Console.ReadLine());

            var items = rest.Get<List<string>>(clubId, "/PlayerStat/PlayerList");

            Console.WriteLine("Player's name list:");
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }

        public void BestAttacker()
        {
            var item = rest.GetSingle<BestAttackerInfo>("/PlayerStat/BestAttacker");
            Console.WriteLine("Club Name" + "\t\t\t" + "Player Name" + "\t\t\t" + "All Goals (Season)");

            Console.WriteLine(item.ClubName + "\t\t\t" + item.PlayerName + "\t\t\t" + item.GoalsInSeason);

            Console.ReadLine();
        }

        public void PlayersAvgAge()
        {
            var age = rest.GetSingle<double?>("/PlayerStat/PlayersAvgAge");
            Console.WriteLine("Players Average Age: " + Math.Round(age.Value, 1));

            Console.ReadLine();
        }

        public void ManagerAvgSal()
        {
            var sal = rest.GetSingle<double?>("/ManagerStat/ManagerAvgSal");
            Console.WriteLine("Manager Average Salary: " + Math.Round(sal.Value, 1) + "M USD/ year");

            Console.ReadLine();
        }

        public void NationList()
        {

            var items = rest.Get<string>("/ClubStat/NationList");

            Console.WriteLine("Nation list:");
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }
    }
}
