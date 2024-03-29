﻿using Q15R74_HFT_2021222.Models;
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


        //Average age

        public double? PlayersAvgAge()
        {
            return this.repo.ReadAll().Average(t => t.Age);
        }



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

            public override bool Equals(object obj)
            {
                ClubAvgAgeInfo b = obj as ClubAvgAgeInfo;
                if (b == null)
                {
                    return false;
                }
                else
                {
                    return this.ClubName == b.ClubName
                        && this.AvgAge == b.AvgAge;
                }
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

        //Which team players got the highest salary summarized ??
        public HighestPaidClubInfo HighestPaidClub()
        {
            return (from x in this.repo.ReadAll()
                    group x by x.Club.Name into g
                    select new HighestPaidClubInfo()
                    {
                        ClubName = g.Key,
                        SalarySum = g.Sum(t => t.Salary)
                    }).OrderByDescending(t => t.SalarySum).First();
        }

        public class HighestPaidClubInfo
        {
            public string ClubName { get; set; }
            public double? SalarySum { get; set; }

            public override bool Equals(object obj)
            {
                HighestPaidClubInfo b = obj as HighestPaidClubInfo;
                if (b == null)
                {
                    return false;
                }
                else
                {
                    return this.ClubName == b.ClubName
                        && this.SalarySum == b.SalarySum;
                }
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

        //Who is the best Manager? (Which managers team scored the most goals?)
        public BestManagerInfo BestManager()
        {
            return (from x in this.repo.ReadAll()
                    group x by x.Club.Manager.Name into g
                    select new BestManagerInfo()
                    {
                        ManagerName = g.Key,
                        AllGoal = g.Sum(t => t.GoalsInSeason),
                        ClubId = g.Sum(t => t.ClubId) / g.Count()

                    }).OrderByDescending(t => t.AllGoal).First();
        }

        public class BestManagerInfo
        {
            public string ManagerName { get; set; }

            public int? AllGoal { get; set; }

            public int? ClubId { get; set; }

            public override bool Equals(object obj)
            {
                BestManagerInfo b = obj as BestManagerInfo;
                if (b == null)
                {
                    return false;
                }
                else
                {
                    return this.ManagerName == b.ManagerName
                        && this.ClubId == b.ClubId
                        && this.AllGoal == b.AllGoal;
                }
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

        //List the player's name in a club
        public IEnumerable<string> PlayerList(int clubID)
        {
            return this.repo
               .ReadAll()
               .Where(t => t.ClubId == clubID)
               .Select(t => t.Name);
        }


        //Which team's attacker scored the most goals?
        public BestAttackerInfo BestAttacker()
        {
            Player best = this.repo.ReadAll()
               .OrderByDescending(t => t.GoalsInSeason)
               .Where(t => t.Positon == Position.Attacker)
               .First();

            return new BestAttackerInfo()
            {
                ClubName = best.Club.Name,
                PlayerName = best.Name,
                GoalsInSeason = best.GoalsInSeason
            };
        }

        public class BestAttackerInfo
        {
            public string ClubName { get; set; }

            public string PlayerName { get; set; }

            public int? GoalsInSeason { get; set; }

            public override bool Equals(object obj)
            {
                BestAttackerInfo b = obj as BestAttackerInfo;
                if (b == null)
                {
                    return false;
                }
                else
                {
                    return this.ClubName == b.ClubName
                        && this.PlayerName == b.PlayerName
                        && this.GoalsInSeason == b.GoalsInSeason;
                }
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

        public IEnumerable<ClubAllGoalsInfo> ClubAllGoals()
        {
            return from x in this.repo.ReadAll()
                   group x by x.ClubId into g
                   select new ClubAllGoalsInfo()
                   {
                       ClubId = g.Key,
                       AllGoals = g.Sum(t => t.GoalsInSeason)
                   };
        }

        public class ClubAllGoalsInfo
        {
            public int ClubId { get; set; }

            public int? AllGoals { get; set; }

            public override bool Equals(object obj)
            {
                ClubAllGoalsInfo b = obj as ClubAllGoalsInfo;
                if (b == null)
                {
                    return false;
                }
                else
                {
                    return this.ClubId == b.ClubId;
                }
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }
    }
}
