using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Q15R74_HFT_2021222.Models
{
    public class Club
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClubId { get; set; }

        [StringLength(240)]
        [Required]
        public string Name { get; set; }

        [ForeignKey(nameof(Manager))]
        public int ManagerId { get; set; }

        [StringLength(240)]
        public string Nation { get; set; }

        [Range(0,10000)]
        // in million USD
        public int? Value { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Player> Players { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Manager Manager { get; set; }

    }
}
