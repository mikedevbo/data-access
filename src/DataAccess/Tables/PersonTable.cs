namespace DataAccess.Tables
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Person")]
    public class PersonTable
    {
        [Key]
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
    }
}
