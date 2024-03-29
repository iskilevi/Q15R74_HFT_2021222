﻿using Q15R74_HFT_2021222.Models;
using System.Collections.Generic;
using System.Linq;

namespace Q15R74_HFT_2021222.Logic
{
    public interface IPlayerLogic
    {
        void Create(Player item);
        void Delete(int id);
        Player Read(int id);
        IQueryable<Player> ReadAll();
        void Update(Player item);
        IEnumerable<PlayerLogic.ClubAvgAgeInfo> ClubAvgAge();

        PlayerLogic.HighestPaidClubInfo HighestPaidClub();

        PlayerLogic.BestManagerInfo BestManager();

        IEnumerable<string> PlayerList(int clubID);

        PlayerLogic.BestAttackerInfo BestAttacker();

        double? PlayersAvgAge();

        IEnumerable<PlayerLogic.ClubAllGoalsInfo> ClubAllGoals();

    }
}