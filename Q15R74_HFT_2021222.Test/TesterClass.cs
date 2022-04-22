using Moq;
using NUnit.Framework;
using Q15R74_HFT_2021222.Logic;
using Q15R74_HFT_2021222.Models;
using Q15R74_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
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

        //ManagerLogic mLogic;
        //Mock<IRepository<Manager>> mockManagerRepo;


        [SetUp]
        public void Init()
        {
            mockPlayerRepo = new Mock<IRepository<Player>>();
            mockClubRepo = new Mock<IRepository<Club>>();


            pLogic = new PlayerLogic(mockPlayerRepo.Object);
            cLogic = new ClubLogic(mockClubRepo.Object);


            //mockPlayerRepo.Setup(m => m.ReadAll()).Returns(new List<Player>()
            //{
            //    new Player(){PlayerId = 1, Name = "Robi"},
            //    new Player(){PlayerId = 2, Name = "Árpád"}

            //}.AsQueryable());
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
    }
}
