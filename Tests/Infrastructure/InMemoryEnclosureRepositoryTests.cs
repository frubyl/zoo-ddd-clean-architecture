using KPO_HW2.Domain.Entities;
using KPO_HW2.Domain.Enum;
using KPO_HW2.Infrastructure.Repositories;
using KPO_HW2.Domain.ValueObject;


namespace Tests.Infrastructure
{
    public class InMemoryEnclosureRepositoryTests
    {
        private readonly InMemoryEnclosureRepository _repository = new InMemoryEnclosureRepository();

        [Fact]
        public async Task AddEnclosureAsync_ShouldAddEnclosureToCollection()
        {
            // Arrange
            var enclosure = new Enclosure(AnimalType.Predator, new Size(10, 10, 5), 5);

            // Act
            await _repository.AddEnclosureAsync(enclosure);

            // Assert
            var result = await _repository.GetEnclosureByIdAsync(enclosure.EnclosureId);
            Assert.Equal(enclosure, result);
        }

        [Fact]
        public async Task AddAnimalToEnclosureAsync_ShouldIncreaseAnimalCount()
        {
            // Arrange
            var enclosure = new Enclosure(AnimalType.Predator, new Size(10, 10, 5), 5);
            await _repository.AddEnclosureAsync(enclosure);
            var animalId = Guid.NewGuid();

            // Act
            await _repository.AddAnimalToEnclosureAsync(animalId, enclosure.EnclosureId);

            // Assert
            var updatedEnclosure = await _repository.GetEnclosureByIdAsync(enclosure.EnclosureId);
            Assert.Equal(1, updatedEnclosure.CurrentAnimalCount);
        }

        [Fact]
        public async Task RemoveAnimalFromEnclosureAsync_ShouldDecreaseAnimalCount()
        {
            // Arrange
            var enclosure = new Enclosure(AnimalType.Predator, new Size(10, 10, 5), 5);
            var animalId = Guid.NewGuid();
            enclosure.AddAnimal(animalId);
            await _repository.AddEnclosureAsync(enclosure);

            // Act
            await _repository.RemoveAnimalFromEnclosureAsync(animalId, enclosure.EnclosureId);

            // Assert
            var updatedEnclosure = await _repository.GetEnclosureByIdAsync(enclosure.EnclosureId);
            Assert.Equal(0, updatedEnclosure.CurrentAnimalCount);
        }

        [Fact]
        public async Task GetFreeEnclosureCountAsync_ShouldReturnCorrectCount()
        {
            // Arrange
            var occupiedEnclosure = new Enclosure(AnimalType.Predator, new Size(10, 10, 5), 5);
            occupiedEnclosure.AddAnimal(Guid.NewGuid());

            var freeEnclosure = new Enclosure(AnimalType.Predator, new Size(8, 8, 4), 3);

            await _repository.AddEnclosureAsync(occupiedEnclosure);
            await _repository.AddEnclosureAsync(freeEnclosure);

            // Act
            var count = await _repository.GetFreeEnclosureCountAsync();

            // Assert
            Assert.Equal(1, count);
        }
    }
}
