using ConsoleTools;
using Q15R74_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Q15R74_HFT_2021222.Client
{

    internal class Program
    {
        static RestService rest;

        static void Create(string entity)
        {
            if (entity == "Player")
            {
                Player p = new Player();

                Console.WriteLine("Type in the player's name: ...");
                string playerName = Console.ReadLine();

                p.Name = playerName;

                Console.WriteLine("Type in the player's age: ...");
                int playerAge = int.Parse(Console.ReadLine());

                p.Age = playerAge;

                Console.WriteLine("Choose player's position: ...");
                Console.WriteLine("-Attacker : a");
                Console.WriteLine("-Midfielder : m");
                Console.WriteLine("-Defender : d");
                string playerPos = Console.ReadLine();
                if (playerPos == "a")
                {
                    p.Positon = Position.Attacker;
                }
                else if (playerPos == "m")
                {
                    p.Positon = Position.Midfielder;

                }
                else if (playerPos == "d")
                {
                    p.Positon = Position.Defender;

                }
                else
                {
                    Console.WriteLine("Incorrect Position!(Position is set to default...)");
                }


                Console.WriteLine("Type in the player's club ID: ...");
                int playerClubId = int.Parse(Console.ReadLine());

                p.ClubId = playerClubId;

                Console.WriteLine("*Player is created!*");
                rest.Post(p, "player");

                Console.ReadKey();
            }
            else if (entity == "Manager")
            {
                Manager m = new Manager();

                Console.WriteLine("Type in the manager's name: ...");
                string managerName = Console.ReadLine();

                m.Name = managerName;

                Console.WriteLine("Type in the manager's salary: ...");
                int managerSalary = int.Parse(Console.ReadLine());

                m.Salary = managerSalary;

                Console.WriteLine("*Manager is created!*");
                rest.Post(m, "manager");

                Console.ReadKey();
            }
            else if (entity == "Club")
            {
                Club c = new Club();

                Console.WriteLine("Type in the club's name: ...");
                string clubName = Console.ReadLine();

                c.Name = clubName;

                Console.WriteLine("Type in the club's nation: ...");
                string clubNation = Console.ReadLine();
                c.Nation = clubNation;

                Console.WriteLine("*Club is created!*");
                rest.Post(c, "club");

                Console.ReadKey();
            }
        }
        static void List(string entity)
        {
            if (entity == "Player")
            {
                List<Player> items = rest.Get<Player>("player");
                Console.WriteLine("Id" + "\t" + "Name" + "\t\t\t\t\t" + "Position" + "\t\t" + "Age");
                foreach (var item in items)
                {
                    Console.WriteLine(item.PlayerId + "\t" + item.Name + "\t\t\t\t" + item.Positon + "\t\t" + item.Age);
                
                }
            }
            else if (entity == "Manager")
            {
                List<Manager> items = rest.Get<Manager>("manager");
                Console.WriteLine("Id" + "\t" + "Name");
                foreach (var item in items)
                {
                     Console.WriteLine(item.ManagerId + "\t" + item.Name);
                }
            }
            else if (entity == "Club")
            {
                List<Club> items = rest.Get<Club>("club");
                Console.WriteLine("Id" + "\t" + "Name" + "\t\t\t\t" + "Nation" + "\t\t" + "Value(M USD)");
                foreach (var item in items)
                {
                     Console.WriteLine(item.ClubId + "\t" + item.Name + "\t\t\t\t" + item.Nation + "\t\t" + item.Value);
                
                }
            }
            Console.ReadLine();
        }
        static void Update(string entity)
        {
            if (entity == "Player")
            {
                Console.WriteLine("Enter player ID to update: ...");

                int playerId = int.Parse(Console.ReadLine());

                var modifiedPlayer = rest.Get<Player>(playerId, "player");
                Console.WriteLine("Current Name: [" + modifiedPlayer.Name + "]");

                Console.WriteLine("Set a new name: ...");
                string name = Console.ReadLine();
                modifiedPlayer.Name = name;


                Console.WriteLine("Current Salary: [" + modifiedPlayer.Salary + "]");
                Console.WriteLine("Update Salary: ...");
                int sal = int.Parse(Console.ReadLine());
                modifiedPlayer.Salary = sal;

                rest.Put(modifiedPlayer, "player");

                Console.WriteLine("*Player is updated!*");
                Console.ReadKey();

            }
            else if (entity == "Manager")
            {
                Console.WriteLine("Enter manager ID to update: ...");

                int managerId = int.Parse(Console.ReadLine());

                var modifiedManager = rest.Get<Manager>(managerId, "manager");
                Console.WriteLine("Current Name: [" + modifiedManager.Name + "]");

                Console.WriteLine("Set a new name: ...");
                modifiedManager.Name = Console.ReadLine();

                rest.Put(modifiedManager, "manager");

                Console.WriteLine("*Manager is updated!*");
                Console.ReadKey();
            }
            else if (entity == "Club")
            {
                Console.WriteLine("Enter club ID to update: ...");

                int clubId = int.Parse(Console.ReadLine());

                var modifiedClub = rest.Get<Club>(clubId, "club");
                Console.WriteLine("Current Name: [" + modifiedClub.Name + "]");

                Console.WriteLine("Set a new name: ...");
                modifiedClub.Name = Console.ReadLine();

                rest.Put(modifiedClub, "club");

                Console.WriteLine("*Club is updated!*");
                Console.ReadKey();
            }
        }
        static void Delete(string entity)
        {
            if (entity == "Player")
            {
                Console.WriteLine("Type in the player ID to delete: ...");

                int playerId = int.Parse(Console.ReadLine());

                rest.Delete(playerId, "player");

                Console.WriteLine("*Player is deleted!*");
                Console.ReadKey();

            }
            else if (entity == "Manager")
            {
                Console.WriteLine("Type in the manager ID to delete: ...");

                int managerId = int.Parse(Console.ReadLine());

                rest.Delete(managerId, "manager");

                Console.WriteLine("*Manager is deleted!*");
                Console.ReadKey();
            }
            else if (entity == "Club")
            {
                Console.WriteLine("Type in the club ID to delete: ...");

                int clubId = int.Parse(Console.ReadLine());

                rest.Delete(clubId, "club");

                Console.WriteLine("*Club is deleted!*");
                Console.ReadKey();
            }
        }

        static void AvgAge()
        {
            List<ClubAvgAgeInfo> items = rest.Get<ClubAvgAgeInfo>("/Stat/ClubAvgAge");
            Console.WriteLine("Name" + "\t\t\t" + "Average Age");
            foreach (var item in items)
            {
                Console.WriteLine(item.ClubName + "\t\t\t" + Math.Round(item.AvgAge.Value, 1));
            }

            Console.ReadLine();
        }

        static void HighestPaid()
        {
            var item = rest.Get<HighestPaidClubInfo>("/Stat/HighestPaidClub");
            Console.WriteLine("Name" + "\t\t\t" + "Salary SUM(M USD)");

            Console.WriteLine(item.First().ClubName + "\t\t\t" + item.First().SalarySum);

            Console.ReadLine();
        }


        static void Main(string[] args)
        {

            rest = new RestService("http://localhost:53910/", "player");


            var clubSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Club"))
                .Add("Create", () => Create("Club"))
                .Add("Delete", () => Delete("Club"))
                .Add("Update", () => Update("Club"))
                .Add("Average Age", () => AvgAge())
                .Add("Highest Paid Club", () => HighestPaid())
                .Add("Exit", ConsoleMenu.Close);

            var managerSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Manager"))
                .Add("Create", () => Create("Manager"))
                .Add("Delete", () => Delete("Manager"))
                .Add("Update", () => Update("Manager"))
                .Add("Exit", ConsoleMenu.Close);

            var playerSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Player"))
                .Add("Create", () => Create("Player"))
                .Add("Delete", () => Delete("Player"))
                .Add("Update", () => Update("Player"))
                .Add("Exit", ConsoleMenu.Close);


            var menu = new ConsoleMenu(args, level: 0)
                .Add("Players", () => playerSubMenu.Show())
                .Add("Managers", () => managerSubMenu.Show())
                .Add("Clubs", () => clubSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();

        }
    }
}
