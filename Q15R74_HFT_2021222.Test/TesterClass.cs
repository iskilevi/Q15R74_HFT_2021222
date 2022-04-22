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

        PlayerLogic logic;
        Mock<IRepository<Player>> mockPlayerRepo;


        [SetUp]
        public void Init()
        {
            mockPlayerRepo = new Mock<IRepository<Player>>();
            mockPlayerRepo.Setup(m => m.ReadAll()).Returns(new List<Player>()
            {
                new Player(){PlayerId = 1, Name = "Robi"},
                new Player(){PlayerId = 2, Name = "Árpád"}

            }.AsQueryable());
            logic = new PlayerLogic(mockPlayerRepo.Object);
        }
    }
}
