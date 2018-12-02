using DataAccess.Tables;
using DataAccess.Views;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DataAccess
{
    public class EF : DbContext, IDataAccessAsync
    {
        public EF() : base(Helper.ConnectionName)
        {
        }

        public DbSet<PersonTable> PersonTable { get; set; }

        public DbSet<PlayerTable> PlayerTable { get; set; }

        public DbSet<PlayersBaseInfoView> PlayersBaseInfoView { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Disable initializer
            Database.SetInitializer<EF>(null);
        }

        public Task AddPlayer(int personId, bool isRightHanded, bool isTwoHandedBackhand)
        {
            var player = new PlayerTable
            {
                Id = personId,
                IsRightHanded = isRightHanded,
                IsTwoHandedBackhand = isTwoHandedBackhand
            };

            this.PlayerTable.Add(player);
            return this.SaveChangesAsync();
        }

        public Task<PlayersBaseInfoView> GetPlayerBaseInfo(int playerId)
        {
            return this.PlayersBaseInfoView.FirstOrDefaultAsync(p => p.Id == playerId);
        }

        public Task SetPlayerCoach(int playerId, int? newCoachId, int? previousCoachId)
        {
            var player = new PlayerTable { Id = playerId };
            this.PlayerTable.Attach(player);

            this.Entry(player).Property(p => p.CoachId).OriginalValue = previousCoachId;
            player.CoachId = newCoachId;

            return this.SaveChangesAsync();
        }
    }
}
