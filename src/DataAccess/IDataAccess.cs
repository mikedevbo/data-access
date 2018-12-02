using DataAccess.Views;

namespace DataAccess
{
    public interface IDataAccess
    {
        PlayersBaseInfoView GetPlayerBaseInfo(int playerId);

        void AddPlayer(int personId, bool IsRightHanded, bool IsTwoHandedBackhand);

        void SetPlayerCoach(int playerId, int? newCoachId, int? previousCoachId);
    }
}
