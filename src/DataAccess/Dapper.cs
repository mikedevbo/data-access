namespace DataAccess
{
    using System.Configuration;
    using System.Data.Entity.Infrastructure;
    using System.Data.SqlClient;
    using System.Linq;
    using DataAccess.Views;
    using global::Dapper;

    public class Dapper : IDataAccess
    {
        public void AddPlayer(int personId, bool isRightHanded, bool isTwoHandedBackhand)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[Helper.ConnectionName].ConnectionString))
            {
                connection.Execute(
                    "insert into dbo.Player(id, IsRightHanded, IsTwoHandedBackhand, CoachId) " +
                    "values(@Id, @IsRightHanded, @IsTwoHandedBackhand, null)",
                    new { Id = personId, isRightHanded, isTwoHandedBackhand });
            }
        }

        public PlayersBaseInfoView GetPlayerBaseInfo(int playerId)
        {
            PlayersBaseInfoView result = null;

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[Helper.ConnectionName].ConnectionString))
            {
                result = connection.Query<PlayersBaseInfoView>(
                    "select " +
                    "  pbi.id" +
                    ", pbi.FirstName" +
                    ", pbi.LastName" +
                    ", pbi.BirthYear" +
                    ", pbi.BirthMonth" +
                    ", pbi.BirthDay" +
                    ", pbi.BirthplaceCountry" +
                    ", pbi.BirthplaceCity" +
                    ", pbi.[Weight]" +
                    ", pbi.Height" +
                    ", pbi.IsRightHanded" +
                    ", pbi.IsTwoHandedBackhand" +
                    ", pbi.CoachId" +
                    ", pbi.CoachFirstName" +
                    ", pbi.CoachLastName " +
                    "from " +
                    "  dbo.PlayersBaseInfo pbi " +
                    "where " +
                    "  pbi.Id = @Id",
                    new { Id = playerId }).FirstOrDefault();
            }

            return result;
        }

        public void SetPlayerCoach(int playerId, int? newCoachId, int? previousCoachId)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[Helper.ConnectionName].ConnectionString))
            {
                var result = connection.Execute(
                    "update " +
                    "   dbo.Player " +
                    "set " +
                    "   CoachId = @newCoachId " +
                    "where Id = @Id and " +
                        string.Format("CoachId {0}", previousCoachId.HasValue ? "= @previousCoachId" : "is null"),
                    new { Id = playerId, newCoachId, previousCoachId });

                if (result == 0)
                {
                    throw new DbUpdateConcurrencyException();
                }
            }
        }
    }
}
