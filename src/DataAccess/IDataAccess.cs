using DataAccess.Tables;
using DataAccess.Views;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IDataAccess
    {
        Task<PlayersBaseInfoView> GetPlayerBaseInfo(int playerId);

        Task AddPlayer(int personId, bool IsRightHanded, bool IsTwoHandedBackhand);

        Task SetPlayerCoach(int playerId, int newCoachId, int? previousCoachId);
    }
}
