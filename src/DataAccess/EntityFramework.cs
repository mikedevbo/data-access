namespace DataAccess
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using DataAccess.Tables;
    using DataAccess.Views;

    public class EntityFramework : DbContext, IDataAccessAsync
    {
        public EntityFramework()
            : base(Helper.ConnectionName)
        {
        }

        public DbSet<PersonTable> PersonTable { get; set; }

        public DbSet<PlayerTable> PlayerTable { get; set; }

        public DbSet<PlayersBaseInfoView> PlayersBaseInfoView { get; set; }

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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ////Disable initializer
            Database.SetInitializer<EntityFramework>(null);
        }
    }
}
