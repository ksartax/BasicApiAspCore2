using BasicApiCore.Models;
using BasicApiCore.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BasicApiCore.Controllers
{
    [Route("/api/castles")]
    public class CastlesController : Controller
    {
        private readonly ICastleRepository _castleRepository;

        public CastlesController(ICastleRepository castleRepository)
        {
            _castleRepository = castleRepository;
        }

        [HttpGet()]
        public IActionResult GetList()
        {
            var castles = _castleRepository.GetCastles();
            var results = AutoMapper.Mapper.Map<IEnumerable<CastleWithoutDetailsDto>>(castles);

            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetCastle(int id, bool includeDetails = false)
        {
            var castle = _castleRepository.GetCastle(id, includeDetails);
            if (castle == null)
            {
                return NotFound();
            }

            if (includeDetails)
            {
                var castleResult = AutoMapper.Mapper.Map<CastleDto>(castle);

                return Ok(castleResult);
            }

            var castleWithoutResult = AutoMapper.Mapper.Map<CastleWithoutDetailsDto>(castle);

            return Ok(castleWithoutResult);
        }
    }
}