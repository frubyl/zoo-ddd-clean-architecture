using KPO_HW2.Domain.Entities;
using KPO_HW2.Domain.Repositories;
using KPO_HW2.Domain.ValueObject;
using KPO_HW2.Presentation.Contracts.Animals;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using KPO_HW2.Domain.Service;
namespace KPO_HW2.Presentation
{
    [ApiController]
    [Route("api/animal")]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalRepository _repository;
        private readonly IAnimalTransferService _animalTransferService;
        public AnimalController(IAnimalRepository repository, IAnimalTransferService animalTransferService)
        {
            _repository = repository;
            _animalTransferService = animalTransferService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnimal(
        [FromBody] CreateAnimalRequest request)
        {
            try
            {
                var species = new Species(request.AnimalType, request.SpeciesName);
                var food = new Food(request.FoodType, request.FoodName); 

                var animal = new Animal(
                    species: species,
                    name: request.Name,
                    birthDate: request.BirthDate,
                    gender: request.Gender,
                    favoriteFood: food,
                    status: request.Status,
                    null);

                await _repository.AddAnimalAsync(animal);

                return StatusCode(201, new { Id = animal.AnimalId });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (JsonException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpDelete("{animalId}")]
        public async Task<IActionResult> DeleteAnimal(Guid animalId)
        {
            try
            {
                await _repository.DeleteAnimalByIdAsync(animalId);
                return NoContent();
            } catch(KeyNotFoundException)
            {
                return NotFound($"Животное с ID {animalId} не найдено");
            }
        }

        [HttpGet("{animalId}")]
        public async Task<IActionResult> GetAnimal(Guid animalId)
        {
            try
            {
                var animal = await _repository.GetAnimalByIdAsync(animalId);
                var response = new GetAnimalResponse(
                   animal.AnimalId,
                   animal.Name,
                   animal.Species.Type,
                   animal.Species.Name,
                   animal.BirthDate,
                   animal.Gender,
                   animal.FavoriteFood.Type,
                   animal.FavoriteFood.Name,
                   animal.Status,
                   animal.EnclosureId,
                   animal.IsHungry);
                return Ok(response);
            } catch (KeyNotFoundException ex)
            {
                return NotFound($"Животное с ID {animalId} не найдено");

            }
        }

        [HttpPut("{animalId}/transfer")]
        public async Task<IActionResult> TransferAnimal(
            Guid animalId,
            [FromBody] TransferAnimalRequest request)
        {
            try
            {
                await _animalTransferService.TransferAnimal(animalId, request.EnclosureId);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Животное или вольер не найдены");
            }
        }
    }
}
