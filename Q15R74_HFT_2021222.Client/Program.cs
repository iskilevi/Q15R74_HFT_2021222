using Q15R74_HFT_2021222.Logic;
using Q15R74_HFT_2021222.Models;
using Q15R74_HFT_2021222.Repository;
using System;
using System.Linq;

namespace Q15R74_HFT_2021222.Client
{
    class Program
    {
        static void Main(string[] args)
        {


            var ctx = new FootballDbContext();
            var repo = new ClubRepository(ctx);
            var logic = new ClubLogic(repo);

            var items = logic.ReadAll();

            Club c = new Club()
            {
                Name = "AB"
            };

            logic.Create(c);

            ;
            Console.ReadKey();
        }
    }
}
