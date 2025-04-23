using KPO_HW2.Domain.Entities;
using KPO_HW2.Domain.ValueObject;
using KPO_HW2.Domain.Enum;

namespace Tests.Domain.Entities
{
    public class EnclosureTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var type = AnimalType.Predator;
            var size = new Size(10, 10, 5);
            var maxCapacity = 5;

            // Act
            var enclosure = new Enclosure(type, size, maxCapacity);

            // Assert
            Assert.NotEqual(Guid.Empty, enclosure.EnclosureId);
            Assert.Equal(type, enclosure.Type);
            Assert.Equal(size, enclosure.Size);
            Assert.Equal(maxCapacity, enclosure.MaxCapacity);
            Assert.False(enclosure.IsClear);
            Assert.Equal(0, enclosure.CurrentAnimalCount);
        }

        [Fact]
        public void AddAnimal_ShouldIncreaseAnimalCount()
        {
            // Arrange
            var enclosure = new Enclosure(AnimalType.Predator, new Size(10, 10, 5), 5);
            var animalId = Guid.NewGuid();

            // Act
            enclosure.AddAnimal(animalId);

            // Assert
            Assert.Equal(1, enclosure.CurrentAnimalCount);
        }

        [Fact]
        public void RemoveAnimal_ShouldDecreaseAnimalCount()
        {
            // Arrange
            var enclosure = new Enclosure(AnimalType.Predator, new Size(10, 10, 5), 5);
            var animalId = Guid.NewGuid();
            enclosure.AddAnimal(animalId);

            // Act
            enclosure.RemoveAnimal(animalId);

            // Assert
            Assert.Equal(0, enclosure.CurrentAnimalCount);
        }

        [Fact]
        public void Clear_ShouldSetIsClearToTrue()
        {
            // Arrange
            var enclosure = new Enclosure(AnimalType.Predator, new Size(10, 10, 5), 5);

            // Act
            enclosure.Clear();

            // Assert
            Assert.True(enclosure.IsClear);
        }
    }
}
