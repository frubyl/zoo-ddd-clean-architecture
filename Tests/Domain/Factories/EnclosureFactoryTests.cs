using KPO_HW2.Domain.Enum;
using KPO_HW2.Domain.Factories;
using KPO_HW2.Domain.ValueObject;


namespace Tests.Domain.Factories
{
    public class EnclosureFactoryTests
    {
        private readonly EnclosureFactory _factory = new EnclosureFactory();
        private readonly Size _testSize = new Size(10, 10, 5);

        [Fact]
        public void CreateEnclosure_ShouldReturnEnclosureWithCorrectProperties()
        {
            // Arrange
            var type = AnimalType.Predator;
            var maxCapacity = 5;

            // Act
            var enclosure = _factory.CreateEnclosure(type, _testSize, maxCapacity);

            // Assert
            Assert.Equal(type, enclosure.Type);
            Assert.Equal(_testSize, enclosure.Size);
            Assert.Equal(maxCapacity, enclosure.MaxCapacity);
            Assert.False(enclosure.IsClear);
            Assert.Equal(0, enclosure.CurrentAnimalCount);
        }

        [Fact]
        public void CreateEnclosure_WithZeroCapacity_ShouldThrowArgumentException()
        {
            // Arrange
            var invalidCapacity = 0;

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                _factory.CreateEnclosure(AnimalType.Predator, _testSize, invalidCapacity));
        }

        [Fact]
        public void CreateEnclosure_WithNegativeCapacity_ShouldThrowArgumentException()
        {
            // Arrange
            var invalidCapacity = -5;

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                _factory.CreateEnclosure(AnimalType.Predator, _testSize, invalidCapacity));
        }
    }
}
