using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicApiCore.Entities
{
    public class Castle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public String Name { get; set; }

        public String Description { get; set; }

        public List<CastleDetail> CastleDetails { get; set; } = new List<CastleDetail>();
    }
}
