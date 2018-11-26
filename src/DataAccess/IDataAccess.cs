using DataAccess.Tables;
using DataAccess.Views;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IDataAccess
    {
        Task<PlayerBaseInfoView> GetPlayerBaseInfo(int playerId);

        int AddPlayer(PersonTable person, PlayerTable player);

        void SetPlayerCoach(int coachId);
    }
}
