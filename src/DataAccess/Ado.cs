namespace DataAccess
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.Entity.Infrastructure;
    using System.Data.SqlClient;
    using DataAccess.Views;

    public class Ado : IDataAccess
    {
        public void AddPlayer(int personId, bool isRightHanded, bool isTwoHandedBackhand)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[Helper.ConnectionName].ConnectionString))
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;

                    command.CommandText =
                        "insert into dbo.Player(id, IsRightHanded, IsTwoHandedBackhand, CoachId) " +
                        "values(@Id, @IsRightHanded, @IsTwoHandedBackhand, null)";

                    command.Parameters.Add("@Id", SqlDbType.Int, 4).Value = personId;
                    command.Parameters.Add("@IsRightHanded", SqlDbType.Bit).Value = isRightHanded;
                    command.Parameters.Add("@IsTwoHandedBackhand", SqlDbType.Bit).Value = isTwoHandedBackhand;

                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
        }

        public PlayersBaseInfoView GetPlayerBaseInfo(int playerId)
        {
            PlayersBaseInfoView result = null;

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[Helper.ConnectionName].ConnectionString))
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;

                    command.CommandText =
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

                    command.Parameters.Add("@Id", SqlDbType.Int, 4).Value = playerId;

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = new PlayersBaseInfoView
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                BirthYear = Convert.ToInt32(reader["BirthYear"]),
                                BirthMonth = Convert.ToInt32(reader["BirthMonth"]),
                                BirthDay = Convert.ToInt32(reader["BirthDay"]),
                                BirthplaceCountry = reader["BirthplaceCountry"].ToString(),
                                BirthplaceCity = reader["BirthplaceCity"].ToString(),
                                Weight = Convert.ToInt32(reader["Weight"]),
                                Height = Convert.ToInt32(reader["Height"]),
                                IsRightHanded = Convert.ToBoolean(reader["IsRightHanded"]),
                                IsTwoHandedBackhand = Convert.ToBoolean(reader["IsTwoHandedBackhand"]),
                                CoachId = reader["CoachId"] == DBNull.Value ? default(int?) : Convert.ToInt32(reader["CoachId"]),
                                CoachFirstName = reader["CoachFirstName"] == DBNull.Value ? null : reader["CoachFirstName"].ToString(),
                                CoachLastName = reader["CoachLastName"] == DBNull.Value ? null : reader["CoachLastName"].ToString(),
                            };
                        }
                    }
                }
            }

            return result;
        }

        public void SetPlayerCoach(int playerId, int? newCoachId, int? previousCoachId)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[Helper.ConnectionName].ConnectionString))
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
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

                    connection.Open();

                    var result = command.ExecuteNonQuery();

                    if (result == 0)
                    {
                        throw new DbUpdateConcurrencyException();
                    }
                }
            }
        }
    }
}
