using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using DataAccess.Views;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
    public class EnterpriseLibrary : IDataAccess
    {
        public void AddPlayer(int personId, bool isRightHanded, bool isTwoHandedBackhand)
        {
            var db = this.GetDatabase();

            using (var command = new SqlCommand())
            {
                command.CommandType = CommandType.Text;

                command.CommandText =
                    "insert into dbo.Player(id, IsRightHanded, IsTwoHandedBackhand, CoachId) " +
                    "values(@Id, @IsRightHanded, @IsTwoHandedBackhand, null)";

                command.Parameters.Add("@Id", SqlDbType.Int, 4).Value = personId;
                command.Parameters.Add("@IsRightHanded", SqlDbType.Bit).Value = isRightHanded;
                command.Parameters.Add("@IsTwoHandedBackhand", SqlDbType.Bit).Value = isTwoHandedBackhand;

                db.ExecuteNonQuery(command);
            }
        }

        public PlayersBaseInfoView GetPlayerBaseInfo(int playerId)
        {
            var db = this.GetDatabase();

            const string sql =
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
                "  pbi.Id = @Id";

            var parameterMapper = new PlayersBaseInfoParameterMapper();
            var accessor = db.CreateSqlStringAccessor<PlayersBaseInfoView>(sql, parameterMapper);

            return accessor.Execute(playerId).FirstOrDefault();
        }

        public void SetPlayerCoach(int playerId, int? newCoachId, int? previousCoachId)
        {
            var db = this.GetDatabase();

            using (var command = new SqlCommand())
            {
                command.CommandType = CommandType.Text;

                command.CommandText =
                    "update " +
                    "   dbo.Player " +
                    "set " +
                    "   CoachId = @newCoachId " +
                    "where Id = @Id and " +
                        string.Format("CoachId {0}", previousCoachId.HasValue ? "= @previousCoachId" : "is null");

                command.Parameters.Add("@Id", SqlDbType.Int, 4).Value = playerId;

                if (newCoachId.HasValue)
                {
                    command.Parameters.Add("@newCoachId", SqlDbType.Int, 4).Value = newCoachId;
                }
                else
                {
                    command.Parameters.Add("@newCoachId", SqlDbType.Int, 4).Value = DBNull.Value;
                }

                if (previousCoachId.HasValue)
                {
                    command.Parameters.Add("@previousCoachId", SqlDbType.Int, 4).Value = previousCoachId.Value;
                }

                var result = db.ExecuteNonQuery(command);

                if (result == 0)
                {
                    throw new DbUpdateConcurrencyException();
                }
            }
        }

        private Database GetDatabase()
        {
            return new DatabaseProviderFactory().Create(Helper.ConnectionName);
        }

        public class PlayersBaseInfoParameterMapper : IParameterMapper
        {
            public void AssignParameters(DbCommand command, object[] parameterValues)
            {
                DbParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@Id";
                parameter.Value = parameterValues[0];
                command.Parameters.Add(parameter);
            }
        }
    }
}
