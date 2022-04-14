﻿using ConsoleTools;
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
            Console.WriteLine(entity + " create");
            Console.ReadLine();
        }
        static void List(string entity)
        {
            if (entity == "Player")
            {
                var items = playerLogic.ReadAll();
                Console.WriteLine("Id" + "\t" + "Name" + "\t\t\t\t\t" + "Position" + "\t\t" + "Age");
                foreach (var item in items)
                {
                    Console.WriteLine(item.PlayerId + "\t" + item.Name + "\t\t\t\t" + item.Positon + "\t\t" + item.Age);
                }
            }
            else if (entity == "Manager")
            {
                var items = managerLogic.ReadAll();
                Console.WriteLine("Id" + "\t" + "Name" + "\t\t\t\t" + "Club");
                foreach (var item in items)
                {
                    Console.WriteLine(item.ManagerId + "\t" + item.Name + "\t\t\t\t" + item.Club.Name);
                }
            }
            else if (entity == "Club")
            {
                var items = clubLogic.ReadAll();
                Console.WriteLine("Id" + "\t" + "Name" + "\t\t\t\t" + "Nation" + "\t\t" + "Manager" + "\t\t\t\t" + "Number of players");
                foreach (var item in items)
                {
                    Console.WriteLine(item.ClubId + "\t" + item.Name + "\t\t\t\t" + item.Nation + "\t\t" + item.Manager.Name + "\t\t\t\t" + item.Players.Count());
                }
            }
            Console.ReadLine();
        }
        static void Update(string entity)
        {
            Console.WriteLine(entity + " update");
            Console.ReadLine();
        }
        static void Delete(string entity)
        {
            Console.WriteLine(entity + " delete");
            Console.ReadLine();
        }

        static void AvgAge(string entity)
        {
            if (entity == "Club")
            {
                var items = playerLogic.ClubAvgAge();
                Console.WriteLine("Id" + "\t" + "Average Age");
                foreach (var item in items)
                {
                    Console.WriteLine(item.ClubId + "\t" + Math.Round(item.AvgAge.Value, 1));
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