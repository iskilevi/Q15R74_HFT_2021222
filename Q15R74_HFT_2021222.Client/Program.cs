using ConsoleTools;
using Q15R74_HFT_2021222.Logic;
using Q15R74_HFT_2021222.Models;
using Q15R74_HFT_2021222.Repository;
using System;
using System.Linq;

namespace Q15R74_HFT_2021222.Client
{
    internal class Program
    {

        static ClubLogic clubLogic;
        static ManagerLogic managerLogic;
        static PlayerLogic playerLogic;

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
                playerLogic.Create(p);

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
                managerLogic.Create(m);

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

                Console.WriteLine("Type in the club's manager ID (Type 0 if club has no manager yet): ...");
                int clubManagerId = int.Parse(Console.ReadLine());
                if (clubManagerId != 0)
                {
                    c.ManagerId = clubManagerId;
                }
                else
                {
                    Console.WriteLine("Club has no manager!");
                }

                Console.WriteLine("*Club is created!*");
                clubLogic.Create(c);

                Console.ReadKey();
            }
        }
        static void List(string entity)
        {
            if (entity == "Player")
            {
                var items = playerLogic.ReadAll();
                Console.WriteLine("Id" + "\t" + "Name" + "\t\t\t\t\t" + "Position" + "\t\t" + "Age" + "\t\t" + "Club");
                foreach (var item in items)
                {
                    if (item.Club != null)
                    {
                        Console.WriteLine(item.PlayerId + "\t" + item.Name + "\t\t\t\t" + item.Positon + "\t\t" + item.Age + "\t\t" + item.Club.Name);
                    }
                    else
                    {
                        Console.WriteLine(item.PlayerId + "\t" + item.Name + "\t\t\t\t" + item.Positon + "\t\t" + item.Age + "\t\t" + "Uknown");
                    }
                }
            }
            else if (entity == "Manager")
            {
                var items = managerLogic.ReadAll();
                Console.WriteLine("Id" + "\t" + "Name" + "\t\t\t\t" + "Club");
                foreach (var item in items)
                {
                    if (item.Club != null)
                    {
                        Console.WriteLine(item.ManagerId + "\t" + item.Name + "\t\t\t\t" + item.Club.Name);
                    }
                    else
                    {
                        Console.WriteLine(item.ManagerId + "\t" + item.Name + "\t\t\t\t" + "Uknown");
                    }
                }
            }
            else if (entity == "Club")
            {
                var items = clubLogic.ReadAll();
                Console.WriteLine("Id" + "\t" + "Name" + "\t\t\t\t" + "Nation" + "\t\t" + "Manager" + "\t\t\t\t" + "Number of players");
                foreach (var item in items)
                {
                    if (item.Players != null)
                    {
                        if (item.Manager != null)
                        {
                            Console.WriteLine(item.ClubId + "\t" + item.Name + "\t\t\t\t" + item.Nation + "\t\t" + item.Manager.Name + "\t\t\t\t" + item.Players.Count());
                        }
                        else
                        {
                            Console.WriteLine(item.ClubId + "\t" + item.Name + "\t\t\t\t" + item.Nation + "\t\t" + "Unknown" + "\t\t\t\t" + item.Players.Count());
                        }
                    }
                    else if (item.Manager != null)
                    {
                        Console.WriteLine(item.ClubId + "\t" + item.Name + "\t\t\t\t" + item.Nation + "\t\t" + item.Manager.Name + "\t\t\t\t" + "0");
                    }
                    else
                    {
                        Console.WriteLine(item.ClubId + "\t" + item.Name + "\t\t\t\t" + item.Nation + "\t\t" + "Unknown" + "\t\t\t\t" + "0");
                    }
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

                var modifiedPlayer = playerLogic.Read(playerId);
                Console.WriteLine("Current Name: ["+modifiedPlayer.Name+"]");

                Console.WriteLine("Set a new name: ...");
                modifiedPlayer.Name = Console.ReadLine();

                playerLogic.Update(modifiedPlayer);

                Console.WriteLine("*Player is updated!*");
                Console.ReadKey();
            }
            else if (entity == "Manager")
            {
                Console.WriteLine("Enter manager ID to update: ...");

                int managerId = int.Parse(Console.ReadLine());

                var modifiedManager = managerLogic.Read(managerId);
                Console.WriteLine("Current Name: [" + modifiedManager.Name + "]");

                Console.WriteLine("Set a new name: ...");
                modifiedManager.Name = Console.ReadLine();

                managerLogic.Update(modifiedManager);

                Console.WriteLine("*Manager is updated!*");
                Console.ReadKey();
            }
            else if (entity == "Club")
            {
                Console.WriteLine("Enter club ID to update: ...");

                int clubId = int.Parse(Console.ReadLine());

                var modifiedClub = clubLogic.Read(clubId);
                Console.WriteLine("Current Name: [" + modifiedClub.Name + "]");

                Console.WriteLine("Set a new name: ...");
                modifiedClub.Name = Console.ReadLine();

                clubLogic.Update(modifiedClub);

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

                playerLogic.Delete(playerId);

                Console.WriteLine("*Player is deleted!*");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine(entity + " delete");
                Console.ReadLine();
            }
        }

        static void AvgAge(string entity)
        {
            if (entity == "Club")
            {
                var items = playerLogic.ClubAvgAge();
                Console.WriteLine("Name" + "\t\t\t" + "Average Age");
                foreach (var item in items)
                {
                    Console.WriteLine(item.ClubName + "\t\t\t" + Math.Round(item.AvgAge.Value, 1));
                }
            }
            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            
            var ctx = new FootballDbContext();

            var ClubRepo = new ClubRepository(ctx);
            var ManagerRepo = new ManagerRepository(ctx);
            var PlayerRepo = new PlayerRepository(ctx);

            clubLogic = new ClubLogic(ClubRepo);
            managerLogic = new ManagerLogic(ManagerRepo);
            playerLogic = new PlayerLogic(PlayerRepo);

            var clubSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Club"))
                .Add("Create", () => Create("Club"))
                .Add("Delete", () => Delete("Club"))
                .Add("Update", () => Update("Club"))
                .Add("Average Age", () => AvgAge("Club"))
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
