using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicApiCore.Entities
{
    public class CastleDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public String Description { get; set; }

        [ForeignKey("CastleId")]
        public Castle Castle { get; set; }
        public int CastleId { get; set; }
    }
}
