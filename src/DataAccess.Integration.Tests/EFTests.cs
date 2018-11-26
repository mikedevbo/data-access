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
        public async Task GetPlayerBaseInfo_ForPlayer_PlayerExists()
        {
            // Arrange
            const int playerId = 1;
            var dataAccess = this.GetEF();

            // Act
            var result = await dataAccess.GetPlayerBaseInfo(playerId).ConfigureAwait(false);

            // Assert
            Assert.That(result, Is.Not.Null);
            System.Console.WriteLine(this.serializer.Serialize(result));
        }

        private EF GetEF()
        {
            return new EF();
        }
    }
}
