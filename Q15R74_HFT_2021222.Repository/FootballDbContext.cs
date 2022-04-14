using System;
using Microsoft.EntityFrameworkCore;
using Q15R74_HFT_2021222.Models;

namespace Q15R74_HFT_2021222.Repository
{
    public class FootballDbContext : DbContext
    {
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Manager> Managers { get; set; }

        public FootballDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder
                    .UseLazyLoadingProxies()
                    .UseInMemoryDatabase("football");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Club>(club => club
                .HasMany(club => club.Players)
                .WithOne(player => player.Club)
                .HasForeignKey(player => player.ClubId)
                .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<Manager>(manager => manager
                .HasOne(manager => manager.Club)
                .WithOne(club => club.Manager)
                .HasForeignKey<Club>(club => club.ManagerId)
                .OnDelete(DeleteBehavior.Cascade));


            modelBuilder.Entity<Club>().HasData(new Club[]
            {
                new Club(){ClubId = 1 ,Name = "PSG", ManagerId = 1, Nation = "France", Value = 2500 },
                new Club(){ClubId = 2 ,Name = "Barcelona", ManagerId = 2, Nation = "Spain", Value = 4760 },
                new Club(){ClubId = 3 ,Name = "Liverpool FC", ManagerId = 3, Nation = "England", Value = 4100 },

            });

            modelBuilder.Entity<Player>().HasData(new Player[]
            {
                new Player(){PlayerId = 1 ,Name = "Lionel Messi", Age = 34, Salary = 41, Positon = Position.Attacker, ClubId = 1, GoalsInSeason = 7 },
                new Player(){PlayerId = 2 ,Name = "Georginio Wijnaldum", Age = 31, Salary = 10, Positon = Position.Midfielder, ClubId = 1, GoalsInSeason = 1 },
                new Player(){PlayerId = 3 ,Name = "Sergio Ramos", Age = 35, Salary = 25, Positon = Position.Defender, ClubId = 1, GoalsInSeason = 1 },
                new Player(){PlayerId = 4 ,Name = "Memphis Depay", Age = 28, Salary = 5, Positon = Position.Attacker, ClubId = 2, GoalsInSeason = 10 },
                new Player(){PlayerId = 5 ,Name = "Frenkie de Jong", Age = 24, Salary = 8, Positon = Position.Midfielder, ClubId = 2, GoalsInSeason = 1 },
                new Player(){PlayerId = 6 ,Name = "Samuel Umtiti", Age = 28, Salary = 17, Positon = Position.Defender, ClubId = 2, GoalsInSeason = 0 },
                new Player(){PlayerId = 7 ,Name = "Mohamed Salah", Age = 29, Salary = 20, Positon = Position.Attacker, ClubId = 3, GoalsInSeason = 20 },
                new Player(){PlayerId = 8 ,Name = "Roberto Firmino", Age = 30, Salary = 10, Positon = Position.Midfielder, ClubId = 3, GoalsInSeason = 5 },
                new Player(){PlayerId = 9 ,Name = "Virgil van Dijk", Age = 30, Salary = 15, Positon = Position.Defender, ClubId = 3, GoalsInSeason = 0 }

            });

            modelBuilder.Entity<Manager>().HasData(new Manager[]
            {
                new Manager(){ManagerId = 1, Name = "Mauricio Pochettino", Salary = 8},
                new Manager(){ManagerId = 2, Name = "Xavier Hernández", Salary = 12},
                new Manager(){ManagerId = 3, Name = "Jürgen Klopp", Salary = 19}

            });

        }
    }
}
