namespace DataAccess.Integration.Tests
{
    using System;
    using System.Data.Entity.Infrastructure;
    using System.Web.Script.Serialization;
    using NUnit.Framework;

    [TestFixture]
    public class EnterpriseLibraryTests
    {
        private readonly JavaScriptSerializer serializer = new JavaScriptSerializer();

        [Test]
        public void AddPlayer_NewPalyer_PlayerAdded()
        {
            // Arrange
            const int personId = 1;
            var dataAccess = this.GetDataAccess();

            // Act
            dataAccess.AddPlayer(personId, true, true);

            // Assert
            var result = dataAccess.GetPlayerBaseInfo(personId);
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetPlayerBaseInfo_ForPlayer_PlayerExists()
        {
            // Arrange
            const int playerId = 1;
            var dataAccess = this.GetDataAccess();

            // Act
            var result = dataAccess.GetPlayerBaseInfo(playerId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Console.WriteLine(this.serializer.Serialize(result));
        }

        [TestCase(2, null)]
        [TestCase(3, 2)]
        [TestCase(null, 3)]
        public void SetPlayerCoach_NewPalyer_CoachSet(int? coachId, int? previousCoachId)
        {
            // Arrange
            const int playerId = 1;
            var dataAccess = this.GetDataAccess();

            // Act
            dataAccess.SetPlayerCoach(playerId, coachId, previousCoachId);

            // Assert
            var result = dataAccess.GetPlayerBaseInfo(playerId);
            Assert.That(result.CoachId, Is.EqualTo(coachId));
        }

        [Test]
        public void SetPlayerCoach_OldState_DbUpdateConcurrencyException()
        {
            // Arrange
            const int playerId = 1;
            int? coachId = 1000;
            int? previousCoachId = 999;
            var dataAccess = this.GetDataAccess();

            // Act
            TestDelegate result = () => dataAccess.SetPlayerCoach(playerId, coachId, previousCoachId);

            // Assert
            Assert.Throws<DbUpdateConcurrencyException>(result);
        }

        private EnterpriseLibrary GetDataAccess()
        {
            return new EnterpriseLibrary();
        }
    }
}
