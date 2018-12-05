namespace DataAccess
{
    using System.Threading.Tasks;
    using DataAccess.Tables;
    using DataAccess.Views;

    public interface IDataAccessAsync
    {
        Task<PlayersBaseInfoView> GetPlayerBaseInfo(int playerId);

        Task AddPlayer(int personId, bool isRightHanded, bool isTwoHandedBackhand);

        Task SetPlayerCoach(int playerId, int? newCoachId, int? previousCoachId);
    }
}
