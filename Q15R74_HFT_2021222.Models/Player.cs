using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Q15R74_HFT_2021222.Models
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerId { get; set; }

        [StringLength(240)]
        [Required]
        public string Name { get; set; }

        [Range(0, 100)]
        public int? Age { get; set; }

        [Range(0, 10000)]
        public int? Salary { get; set; }

        public Position Positon { get; set; }

        [ForeignKey(nameof(Club))]
        public int ClubId { get; set; }

        [Range(0, 1000)]
        public int? GoalsInSeason { get; set; }
    }

    public enum Position
    {
        Attacker,
        Midfielder,
        Defender
    }
}
