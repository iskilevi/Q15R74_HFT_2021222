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
    public class Manager
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ManagerId { get; set; }

        [StringLength(240)]
        [Required]
        public string Name { get; set; }

        [Range(0, 10000)]
        public int? Salary { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Club Club { get; set; }
    }
}
