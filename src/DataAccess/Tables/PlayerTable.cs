namespace DataAccess.Tables
{
    public class PlayerTable
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public bool IsRightHandedBackhand { get; set; }

        public bool IsTwoHandedBackhand { get; set; }

        public int CoachId { get; set; }
    }
}
