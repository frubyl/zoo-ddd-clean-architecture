
using KPO_HW2.Presentation.Contracts.FeedingSchedule;
using KPO_HW2.Domain.Entities;
using KPO_HW2.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace KPO_HW2.Presentation.Controllers
{
    [ApiController]
    [Route("api/feeding-schedules")]
    public class FeedingScheduleController : ControllerBase
    {
        private readonly IFeedingScheduleRepository _repository;
        private readonly IAnimalRepository _animalRepository;

        public FeedingScheduleController(
            IFeedingScheduleRepository repository,
            IAnimalRepository animalRepository)
        {
            _repository = repository;
            _animalRepository = animalRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeedingSchedule(
            [FromBody] CreateFeedingScheduleRequest request)
        {
            try
            {
                var animal = await _animalRepository.GetAnimalByIdAsync(request.AnimalId);
                if (animal == null)
                {
                    return NotFound($"Животное с ID {request.AnimalId} не найдено");
                }

                var feedingSchedule = new FeedingSchedule(
                    request.AnimalId,
                    request.FeedingTime,
                    request.FoodType);

                await _repository.AddFeedingScheduleAsync(feedingSchedule);

                return StatusCode(201, new { Id = feedingSchedule.FeedingScheduleId });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("{feedingScheduleId}")]
        public async Task<IActionResult> GetFeedingSchedule(Guid feedingScheduleId)
        {
            try
            {
                var schedule = await _repository.GetFeedingScheduleByIdAsync(feedingScheduleId);
                var response = new GetFeedingScheduleResponse(
                    schedule.FeedingScheduleId,
                    schedule.AnimalId,
                    schedule.FeedingTime,
                    schedule.FoodType,
                    schedule.IsCompleted);

                return Ok(response);
            } catch (KeyNotFoundException)
            {
                return NotFound($"Расписание кормления с ID {feedingScheduleId} не найдено");
            }
        }

        [HttpDelete("{feedingScheduleId}")]
        public async Task<IActionResult> DeleteFeedingSchedule(Guid feedingScheduleId)
        {
            try
            {
                await _repository.DeleteFeedingScheduleByIdAsync(feedingScheduleId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Расписание кормления с ID {feedingScheduleId} не найдено");
            }
        }

        [HttpGet("animal/{animalId}")]
        public async Task<IActionResult> GetFeedingSchedulesByAnimal(Guid animalId)
        {
            try
            {
                var animal = await _animalRepository.GetAnimalByIdAsync(animalId);
                var schedules = await _repository.GetSchedulesByAnimalIdAsync(animalId);

                var response = schedules.Select(s => new GetFeedingScheduleResponse(
                    s.FeedingScheduleId,
                    s.AnimalId,
                    s.FeedingTime,
                    s.FoodType,
                    s.IsCompleted));
                return Ok(response);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Животное с ID {animalId} не найдено");
            }
        }
    }
}
