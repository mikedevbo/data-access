using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Ado
    {
        public PlayerStateView GetPlayerTournamentsActivity(int playerId)
        {
            //coś jak podsumowanie na początku mecznów -> ile razy zagrał, ile wygrał, ile przegrał zarobki, ...
            return new PlayerStateView();
        }

        public int RegisterPlayerInTournament(int playerId, int tournamentId, int year)
        {
            return 0;
        }

        public void WithdrawPlayerFromTournament(int playerId, int tournamentId, string reason)
        {
            //update with concurrency -> rowVersion
        }
    }
}
