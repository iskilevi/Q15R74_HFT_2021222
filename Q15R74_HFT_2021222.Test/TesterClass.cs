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

        ManagerLogic mLogic;
        Mock<IRepository<Manager>> mockManagerRepo;


        [SetUp]
        public void Init()
        {

            mockPlayerRepo = new Mock<IRepository<Player>>();
            mockClubRepo = new Mock<IRepository<Club>>();
            mockManagerRepo = new Mock<IRepository<Manager>>();


            // Fake Clubs
            var c1 = new Club()
            {
                ClubId = 1,
                Name = "Beton FC",
                Nation = "Hungary",
                ManagerId = 1,
                Value = 200,
                Manager = new Manager()
                {
                    ManagerId = 1,
                    Name = "Béla",
                    Salary = 5,
                },
                Players = new List<Player>()
                {
                    new Player()
                    {
                        PlayerId = 1,
                        Name = "Robi",
                        Age = 20,
                        ClubId = 1,
                        Salary = 100,
                        GoalsInSeason = 1,
                        Positon = Position.Attacker
                  },

                    new Player()
                    {
                        PlayerId = 2,
                        Name = "Gabi",
                        Age = 30,
                        ClubId = 1,
                        Salary = 25,
                        GoalsInSeason = 4,
                        Positon = Position.Midfielder

                    }


                }
            };

            var c2 = new Club()
            {
                ClubId = 2,
                Name = "Fradi",
                Nation = "Hungary",
                ManagerId = 2,
                Manager = new Manager()
                {
                    ManagerId = 2,
                    Name = "Roberto",
                    Salary = 10,
                },
                Players = new List<Player>()
                {
                    new Player()
                    {
                        PlayerId = 3,
                        Name = "Árpád",
                        Age = 30,
                        ClubId = 2,
                        GoalsInSeason = 1,
                    }
                }
            };

            var c3 = new Club()
            {
                ClubId = 3,
                Name = "RB Liepzig",
                Nation = "Germany"
            };

            //Fake players
            var p1 = new Player()
            {
                PlayerId = 1,
                Name = "Robi",
                Age = 20,
                ClubId = 1,
                Salary = 100,
                GoalsInSeason = 1,
                Positon = Position.Attacker,
                Club = c1
            };

            var p2 = new Player()
            {
                PlayerId = 2,
                Name = "Gabi",
                Age = 30,
                ClubId = 1,
                Salary = 25,
                GoalsInSeason = 4,
                Positon = Position.Midfielder,
                Club = c1
            };

            var p3 = new Player()
            {
                PlayerId = 3,
                Name = "Árpád",
                Age = 30,
                ClubId = 2,
                GoalsInSeason = 1,
                Club = c2
            };

            //Fake managers

            var m1 = new Manager()
            {
                ManagerId = 1,
                Name = "Béla",
                Salary = 5,
                Club = c1
            };

            var m2 = new Manager()
            {
                ManagerId = 2,
                Name = "Roberto",
                Salary = 10,
                Club = c2
            };

            mockPlayerRepo.Setup(m => m.ReadAll()).Returns(new List<Player>()
            {
                p1, p2, p3
            }.AsQueryable());


            mockClubRepo.Setup(m => m.ReadAll()).Returns(new List<Club>()
            {
                c1, c2, c3
            }.AsQueryable());

            mockManagerRepo.Setup(m => m.ReadAll()).Returns(new List<Manager>()
            {
                m1, m2
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
        public void CreateManagerTestWithCorrectName()
        {
            var manager = new Manager() { Name = "Xavi", Salary = 10 };

            //ACT
            mLogic.Create(manager);

            //ASSERT
            mockManagerRepo.Verify(r => r.Create(manager), Times.Once);
        }

        [Test]
        public void CreateManagerTestWithInCorrectName()
        {
            var manager = new Manager() { Name = "X" };
            try
            {
                //ACT
                mLogic.Create(manager);
            }
            catch
            {

            }

            //ASSERT
            mockManagerRepo.Verify(r => r.Create(manager), Times.Never);
        }

        [Test]
        public void ReadClubTestWithInCorrectId()
        {
            int id = 5;

            try
            {
                //ACT
                cLogic.Read(id);
            }
            catch
            {

            }

            //ASSERT
            Assert.That(() => cLogic.Read(id),
                Throws.TypeOf<ArgumentException>().With.Message
                      .EqualTo("Club does not exist..."));
        }

        [Test]
        public void ReadAllManagerTest()
        {
            var actual = mLogic.ReadAll();

            var expected = new List<Manager>()
            {
                new Manager() { ManagerId = 1, Name = "Béla", Salary = 5 },
                new Manager() { ManagerId = 2, Name = "Roberto", Salary = 10 }
            };

            Assert.AreEqual(expected, actual);

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
            var actual = Math.Round(pLogic.PlayersAvgAge().Value, 1);
            var expected = 26.7;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ManagerAvgSalTest()
        {
            var actual = mLogic.ManagerAvgSal();
            var expected = 7.5;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void NationListTest()
        {
            var actual = cLogic.NationList();
            var expected = new List<string>()
            {
                   "Hungary", "Germany"
            };

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ClubAllGoals()
        {
            var actual = pLogic.ClubAllGoals();
            var expected = new List<PlayerLogic.ClubAllGoalsInfo>()
            {
               new PlayerLogic.ClubAllGoalsInfo
               {
                   ClubId = 1,
                   AllGoals = 5
               },

               new PlayerLogic.ClubAllGoalsInfo
               {
                   ClubId = 2,
                   AllGoals = 1
               }

            };

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ClubAvgAgeTest()
        {
            var actual = pLogic.ClubAvgAge().ToList();
            var expected = new List<PlayerLogic.ClubAvgAgeInfo>()
            {
               new PlayerLogic.ClubAvgAgeInfo
               {
                   ClubName = "Beton FC",
                   AvgAge = 25
               },
               new PlayerLogic.ClubAvgAgeInfo
               {
                   ClubName = "Fradi",
                   AvgAge = 30
               },
            };

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void HighestPaidClubTest()
        {
            var actual = pLogic.HighestPaidClub();
            var expected = new PlayerLogic.HighestPaidClubInfo()
            {
                ClubName = "Beton FC",
                SalarySum = 125
            };

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void BestManagerTest()
        {
            var actual = pLogic.BestManager();
            var expected = new PlayerLogic.BestManagerInfo()
            {
                ManagerName = "Béla",
                ClubId = 1,
                AllGoal = 5
            };

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void BestAttackerTest()
        {
            var actual = pLogic.BestAttacker();
            var expected = new PlayerLogic.BestAttackerInfo()
            {
                ClubName = "Beton FC",
                PlayerName = "Robi",
                GoalsInSeason = 1
            };

            Assert.That(actual, Is.EqualTo(expected));
        }

    }
}
