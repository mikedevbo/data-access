namespace DataAccess.Integration.Tests
{
    using System.Data.Entity.Infrastructure;
    using System.Threading.Tasks;
    using System.Web.Script.Serialization;
    using DataAccess.Views;
    using NUnit.Framework;

    [TestFixture]
    public class EntityFrameworkTests
    {
        private readonly JavaScriptSerializer serializer = new JavaScriptSerializer();

        [Test]
        public async Task AddPlayer_NewPalyer_PlayerAdded()
        {
            // Arrange
            const int personId = 1;
            PlayersBaseInfoView result = null;

            // Act
            using (var dataAccess = this.GetDataAccess())
            {
                await dataAccess.AddPlayer(personId, true, true).ConfigureAwait(false);
            }

            // Assert
            using (var dataAccess = this.GetDataAccess())
            {
                result = await dataAccess.GetPlayerBaseInfo(personId).ConfigureAwait(false);
            }

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task GetPlayerBaseInfo_ForPlayer_PlayerExists()
        {
            // Arrange
            const int playerId = 1;
            PlayersBaseInfoView result = null;

            // Act
            using (var dataAccess = this.GetDataAccess())
            {
                result = await dataAccess.GetPlayerBaseInfo(playerId).ConfigureAwait(false);
            }

            // Assert
            Assert.That(result, Is.Not.Null);
            System.Console.WriteLine(this.serializer.Serialize(result));
        }

        [TestCase(2, null)]
        [TestCase(3, 2)]
        [TestCase(null, 3)]
        public async Task SetPlayerCoach_NewPalyer_CoachSet(int? coachId, int? previousCoachId)
        {
            // Arrange
            const int playerId = 1;
            PlayersBaseInfoView result = null;

            // Act
            using (var dataAccess = this.GetDataAccess())
            {
                await dataAccess.SetPlayerCoach(playerId, coachId, previousCoachId).ConfigureAwait(false);
            }

            // Assert
            using (var dataAccess = this.GetDataAccess())
            {
                result = await dataAccess.GetPlayerBaseInfo(playerId).ConfigureAwait(false);
            }

            Assert.That(result.CoachId, Is.EqualTo(coachId));
        }

        [Test]
        public Task SetPlayerCoach_OldState_DbUpdateConcurrencyException()
        {
            // Arrange
            const int playerId = 1;
            int? coachId = 1000;
            int? previousCoachId = 999;

            // Act
            AsyncTestDelegate result = async () =>
            {
                using (var dataAccess = this.GetDataAccess())
                {
                    await dataAccess.SetPlayerCoach(playerId, coachId, previousCoachId).ConfigureAwait(false);
                }
            };

            // Assert
            Assert.ThrowsAsync<DbUpdateConcurrencyException>(result);

            return Task.CompletedTask;
        }

        private EntityFramework GetDataAccess()
        {
            return new EntityFramework();
        }
    }
}
