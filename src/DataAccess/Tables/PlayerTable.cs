namespace DataAccess.Tables
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Player")]
    public class PlayerTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public bool IsRightHanded { get; set; }

        public bool IsTwoHandedBackhand { get; set; }

        [ConcurrencyCheck]
        public int? CoachId { get; set; }
    }
}
