using DataAccess.Tables;
using DataAccess.Views;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DataAccess
{
    public class EF : DbContext, IDataAccess
    {
        public EF() : base("name=Players")
        {
        }

        public DbSet<PlayerBaseInfoView> PlayerBaseInfoView { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Disable initializer
            Database.SetInitializer<DbContext>(null);
        }

        public int AddPlayer(PersonTable person, PlayerTable player)
        {
            throw new System.NotImplementedException();
        }

        public Task<PlayerBaseInfoView> GetPlayerBaseInfo(int playerId)
        {
            return this.PlayerBaseInfoView.FirstOrDefaultAsync(p => p.Id == playerId);
        }

        public void SetPlayerCoach(int coachId)
        {
            throw new System.NotImplementedException();
        }
    }
}
