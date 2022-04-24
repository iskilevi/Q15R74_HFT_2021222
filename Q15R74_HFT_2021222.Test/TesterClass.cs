using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Q15R74_HFT_2021222.Logic;
using Q15R74_HFT_2021222.Models;
using Q15R74_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace Q15R74_HFT_2021222.Test
{

    [TestFixture]
    public class TesterClass
    {

        PlayerLogic pLogic;
        Mock<IRepository<Player>> mockPlayerRepo;

        ClubLogic cLogic;
        Mock<IRepository<Club>> mockClubRepo;

        ManagerLogic mLogic;
        Mock<IRepository<Manager>> mockManagerRepo;


        [SetUp]
        public void Init()
        {

            mockPlayerRepo = new Mock<IRepository<Player>>();
            mockClubRepo = new Mock<IRepository<Club>>();
            mockManagerRepo = new Mock<IRepository<Manager>>();
            

            mockPlayerRepo.Setup(m => m.ReadAll()).Returns(new List<Player>()
            {
                new Player(){PlayerId = 1, Name = "Robi", Age = 20, ClubId = 1, Salary = 100,},
                new Player(){PlayerId = 2, Name = "Gabi", Age = 25, ClubId = 1, Salary = 25,},
                new Player(){PlayerId = 3, Name = "Árpád", Age = 30, ClubId = 2}

            }.AsQueryable());


            mockClubRepo.Setup(m => m.ReadAll()).Returns(new List<Club>()
            {
                new Club(){ClubId =1, Name ="Beton FC" },
                new Club(){ClubId = 2, Name = "Fradi" }

            }.AsQueryable());

            mockManagerRepo.Setup(m => m.ReadAll()).Returns(new List<Manager>()
            {
                new Manager(){ManagerId = 1, Name = "Béla" },
                new Manager(){ManagerId = 2, Name = "Roberto"}

            }.AsQueryable());

            pLogic = new PlayerLogic(mockPlayerRepo.Object);
            cLogic = new ClubLogic(mockClubRepo.Object);
            mLogic = new ManagerLogic(mockManagerRepo.Object);

        }

        [Test]
        public void CreatePlayerTestWithCorrectName()
        {
            var player = new Player() { Name = "Alberto" };

            //ACT
            pLogic.Create(player);

            //ASSERT
            mockPlayerRepo.Verify(r => r.Create(player), Times.Once);
        }

        [Test]
        public void CreatePlayerTestWithInCorrectName()
        {
            var player = new Player() { Name = "22" };
            try
            {
                //ACT
                pLogic.Create(player);
            }
            catch
            {

            }

            //ASSERT
            mockPlayerRepo.Verify(r => r.Create(player), Times.Never);
        }

        [Test]
        public void CreateClubTestWithInCorrectName()
        {
            var club = new Club() { Name = "a" };
            try
            {
                //ACT
                cLogic.Create(club);
            }
            catch
            {

            }

            //ASSERT
            mockClubRepo.Verify(r => r.Create(club), Times.Never);
        }

        [Test]
        public void PlayerListTest()
        {
            var actual = pLogic.PlayerList(1);
            var expected = new List<string>()
                {
                   "Robi", "Gabi"
                };

            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void PlayersAvgAgeTest()
        {
            var actual = pLogic.PlayersAvgAge();
            var expected = 25;

            Assert.AreEqual(expected, actual);
        }
    }
}
