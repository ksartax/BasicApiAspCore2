using System;
using System.ComponentModel.DataAnnotations;

namespace BasicApiCore.Models
{
    public class CastleDetailForCreateDto
    {
        [Required(ErrorMessage = "Pole wymagane")]
        public String Description { get; set; }
    }
}
