namespace DataAccess
{
    using DataAccess.Views;

    public interface IDataAccess
    {
        PlayersBaseInfoView GetPlayerBaseInfo(int playerId);

        void AddPlayer(int personId, bool isRightHanded, bool isTwoHandedBackhand);

        void SetPlayerCoach(int playerId, int? newCoachId, int? previousCoachId);
    }
}
