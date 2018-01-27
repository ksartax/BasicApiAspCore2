using System.Collections.Generic;
using System.Linq;
using BasicApiCore.Entities;
using BasicApiCore.Models;
using BasicApiCore.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BasicApiCore.Controllers
{
    [Route("/api/castle")]
    public class CastleDetailsController : Controller
    {
        private readonly ICastleRepository _castleRepository;
        private ILogger<CastleDetailsController> _logger;
        private IMailService _mailService;

        public CastleDetailsController(ILogger<CastleDetailsController> logger, 
            IMailService mailService,
            ICastleRepository castleRepository)
        {
            _logger = logger;
            _mailService = mailService;
            _castleRepository = castleRepository;
        }

        [HttpGet("{castleId}/details")]
        public IActionResult GetCastleDetails(int castleId)
        {
            if (!_castleRepository.CastleExist(castleId))
            {
                _logger.LogCritical($"Castle o id {castleId} znalezione");
                return NotFound();
            }

            var castleDetails = _castleRepository.GetCastleDetailsForCastle(castleId);
            var castleDetailsResult = AutoMapper.Mapper.Map<IEnumerable<CastleDetailDto>>(castleDetails);

            return Ok(castleDetailsResult);
        }

        [HttpGet("{castleId}/detail/{Id}", Name = "GetCastleDetail")]
        public IActionResult GetCastleDetail(int castleId, int id)
        {
            if (!_castleRepository.CastleExist(castleId))
            {
                _logger.LogCritical($"Castle o id {castleId} znalezione");
                return NotFound();
            }

            var castleDetail = _castleRepository.GetCastleDetailForCastle(castleId, id);
            if (castleDetail == null)
            {
                return NotFound();
            }

            var detailResult = AutoMapper.Mapper.Map<CastleDetailDto>(castleDetail);

            return Ok(detailResult);
        }

        [HttpPost("{castleId}/details")]
        public IActionResult Create([FromBody] CastleDetailForCreateDto castleDetails, int castleId)
        {
            if (castleDetails == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_castleRepository.CastleExist(castleId))
            {
                return NotFound();
            }

            if (castleDetails.Description == "Admin")
            {
                ModelState.AddModelError("Description", "Pole nie może mieć nazwy admin");
            }

            var finalCastleDetail = AutoMapper.Mapper.Map<CastleDetail>(castleDetails);
            _castleRepository.AddCastleDetailForCastle(castleId ,finalCastleDetail);

            if (!_castleRepository.Save())
            {
                return StatusCode(500, "Błąd zapisu");
            }

            var createCastleDetailsReturn = AutoMapper.Mapper.Map<Models.CastleDetailDto>(finalCastleDetail);

            return CreatedAtRoute("GetCastleDetail", new { castleId = castleId, id = finalCastleDetail.Id }, createCastleDetailsReturn);
        }

        [HttpDelete("{castleId}/detail/{id}")]
        public IActionResult Delete(int castleId, int id)
        {
            if (!_castleRepository.CastleExist(castleId))
            {
                return NotFound();
            }

            var detail = _castleRepository.GetCastleDetailForCastle(castleId, id);
            if (detail == null)
            {
                return NotFound();
            }

            _castleRepository.DeleteDetail(detail);
            if (!_castleRepository.Save())
            {
                return StatusCode(500, "Błąd zapisu");
            }

            return NoContent();
        }

        [HttpPut("{castleId}/detail/{id}")]
        public IActionResult Update(int castleId, int id, 
            [FromBody] CastleDetailForUpdateDto castleDetails)
        {
            if (castleDetails == null)
            {
                return BadRequest();
            }

            if (castleDetails.Description == "Admin")
            {
                ModelState.AddModelError("Description", "Pole nie może mieć nazwy admin");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_castleRepository.CastleExist(castleId))
            {
                return NotFound();
            }

            var detail = _castleRepository.GetCastleDetailForCastle(castleId, id);
            if (detail == null)
            {
                return NotFound();
            }

            AutoMapper.Mapper.Map(castleDetails, detail);

            if (!_castleRepository.Save())
            {
                return StatusCode(500, "Błąd zapisu");
            }

            return NoContent();
        }

        [HttpPatch("{castleId}/detail/{id}")]
        public IActionResult Update(int castleId, int id, 
            [FromBody] JsonPatchDocument<CastleDetailForUpdateDto> jsonPatchDocument)
        {
            if (jsonPatchDocument == null)
            {
                return BadRequest();
            }

            if (!_castleRepository.CastleExist(castleId))
            {
                return NotFound();
            }
             
            var detail = _castleRepository.GetCastleDetailForCastle(castleId, id);
            if (detail == null)
            {
                return NotFound();
            }

            var detailToPatch = AutoMapper.Mapper.Map<CastleDetailForUpdateDto>(detail);

            jsonPatchDocument.ApplyTo(detailToPatch, ModelState);

            TryValidateModel(detailToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AutoMapper.Mapper.Map(detailToPatch, detail);

            if (!_castleRepository.Save())
            {
                return StatusCode(500, "Błąd zapisu");
            }

            return NoContent();
        }
    }
}