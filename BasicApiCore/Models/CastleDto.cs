using System;
using System.Collections.Generic;

namespace BasicApiCore.Models
{
    public class CastleDto
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public List<CastleDetailDto> CastleDetails;
    }
}
