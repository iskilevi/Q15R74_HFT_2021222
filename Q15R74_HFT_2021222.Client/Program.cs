using Q15R74_HFT_2021222.Repository;
using System;
using System.Linq;

namespace Q15R74_HFT_2021222.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Football Db:");

            FootballDbContext ctx = new FootballDbContext();

            var items = ctx.Players.ToArray();

            ;
            Console.ReadKey();
        }
    }
}
