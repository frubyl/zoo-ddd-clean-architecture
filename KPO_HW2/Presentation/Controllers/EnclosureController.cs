using KPO_HW2.Domain.ValueObject;
using KPO_HW2.Domain.Repositories;
using KPO_HW2.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using KPO_HW2.Presentation.Contracts.Enclosure;


namespace KPO_HW2.Presentation.Controllers
{
    [ApiController]
    [Route("api/enclosures")]
    public class EnclosureController : ControllerBase
    {
        private readonly IEnclosureRepository _repository;

        public EnclosureController(IEnclosureRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEnclosure(
            [FromBody] CreateEnclosureRequest request)
        {
            try
            {
                var size = new Size(request.Length, request.Width, request.Height);
                var enclosure = new Enclosure(
                    request.AnimalType,
                    size,
                    request.MaxCapacity);

                await _repository.AddEnclosureAsync(enclosure);

                return StatusCode(201, new { Id = enclosure.EnclosureId });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("{enclosureId}")]
        public async Task<IActionResult> GetEnclosure(Guid enclosureId)
        {
            try
            {
                var enclosure = await _repository.GetEnclosureByIdAsync(enclosureId);
                var response = new GetEnclosureResponse(
                    enclosure.EnclosureId,
                    enclosure.Type,
                    enclosure.Size.Length,
                    enclosure.Size.Width,
                    enclosure.Size.Height,
                    enclosure.CurrentAnimalCount,
                    enclosure.MaxCapacity,
                    enclosure.IsClear);

                return Ok(response);
            } catch (KeyNotFoundException)
            {
                return NotFound($"Вольер с ID {enclosureId} не найден");
            }
        }

        [HttpDelete("{enclosureId}")]
        public async Task<IActionResult> DeleteEnclosurel(Guid enclosureId)
        {
            try
            {
                await _repository.DeleteEnclosureByIdAsync(enclosureId);
                return NoContent();
            } catch (KeyNotFoundException)
            {
                return NotFound($"Вольер с ID {enclosureId} не найден");
            }
        }
    }
}

