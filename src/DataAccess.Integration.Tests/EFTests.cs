using DataAccess.Views;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace DataAccess.Integration.Tests
{
    [TestFixture]
    public class EFTests
    {
        private readonly JavaScriptSerializer serializer = new JavaScriptSerializer();

        [Test]
        public async Task AddPlayer_NewPalyer_NewPlayerId()
        {
            // Arrange
            const int personId = 1;

            // Act
            using (var dataAccess = this.GetEF())
            {
                await dataAccess.AddPlayer(personId, true, true).ConfigureAwait(false);
            }

            // Assert
            ////No Exception
        }

        [Test]
        public async Task GetPlayerBaseInfo_ForPlayer_PlayerExists()
        {
            // Arrange
            const int playerId = 1;
            PlayersBaseInfoView result = null;

            // Act
            using (var dataAccess = this.GetEF())
            {
                result = await dataAccess.GetPlayerBaseInfo(playerId).ConfigureAwait(false);
            }

            // Assert
            Assert.That(result, Is.Not.Null);
            System.Console.WriteLine(this.serializer.Serialize(result));
        }

        [Test]
        public async Task SetPlayerCoach_NewPalyer_NewPlayerId()
        {
            // Arrange
            const int playerId = 1;
            const int coachId = 1;
            int? previousCoachId = 2;

            // Act
            using (var dataAccess = this.GetEF())
            {
                await dataAccess.SetPlayerCoach(playerId, coachId, previousCoachId).ConfigureAwait(false);
            }

            // Assert
            ////No Exception
        }

        private EF GetEF()
        {
            return new EF();
        }
    }
}
