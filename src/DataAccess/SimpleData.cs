namespace DataAccess
{
    using System.Data.Entity.Infrastructure;
    using DataAccess.Tables;
    using DataAccess.Views;
    using Simple.Data;

    public class SimpleData : IDataAccess
    {
        public void AddPlayer(int personId, bool isRightHanded, bool isTwoHandedBackhand)
        {
            var player = new PlayerTable
            {
                Id = personId,
                IsRightHanded = isRightHanded,
                IsTwoHandedBackhand = isTwoHandedBackhand
            };

            dynamic db = this.GetDatabase();
            db.Player.Insert(player);
        }

        public PlayersBaseInfoView GetPlayerBaseInfo(int playerId)
        {
            dynamic db = this.GetDatabase();
            return db.PlayersBaseInfo.FindAllById(playerId).FirstOrDefault();
        }

        public void SetPlayerCoach(int playerId, int? newCoachId, int? previousCoachId)
        {
            dynamic db = this.GetDatabase();

            var result = db.Player.UpdateAll(
                CoachId: newCoachId,
                Condition: (db.Player.Id == playerId) && (db.Player.CoachId == previousCoachId));

            if (result.ReturnValue == 0)
            {
                throw new DbUpdateConcurrencyException();
            }
        }

        private Database GetDatabase()
        {
            return Database.OpenNamedConnection(Helper.ConnectionName);
        }
    }
}
