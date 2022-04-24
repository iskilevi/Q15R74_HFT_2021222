using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        //in million USD
        public double? Salary { get; set; }

        public Position Positon { get; set; }

        [ForeignKey(nameof(Club))]
        public int ClubId { get; set; }

        [Range(0, 1000)]
        public int? GoalsInSeason { get; set; }
        
        [NotMapped]
        [JsonIgnore]
        public virtual Club Club { get; set; }
    }

    public enum Position
    {
        Attacker,
        Midfielder,
        Defender
    }

    public class ClubAvgAgeInfo
    {
        public string ClubName { get; set; }
        public double? AvgAge { get; set; }

    }

    public class HighestPaidClubInfo
    {
        public string ClubName { get; set; }
        public double? SalarySum { get; set; }
    }

    public class BestManagerInfo
    {
        public string ManagerName { get; set; }

        public int? AllGoal { get; set; }

        public int? ClubId { get; set; }
    }

    public class BestAttackerInfo
    {
        public string ClubName { get; set; }

        public string PlayerName { get; set; }

        public int? GoalsInSeason { get; set; }
    }
}
