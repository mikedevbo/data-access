namespace DataAccess.Views
{
    public class PlayerBaseInfoView
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int BirthYear { get; set; }

        public int BirthMonth { get; set; }

        public int BirthDay { get; set; }

        public string BirthplaceCountry { get; set; }

        public string BirthplaceCity { get; set; }

        public int Weight { get; set; }

        public int Height { get; set; }

        public bool IsRightHandedBackhand { get; set; }

        public bool IsTwoHandedBackhand { get; set; }

        public int CoachId { get; set; }

        public string CoachFirstName { get; set; }

        public string CoachLastName { get; set; }
    }
}