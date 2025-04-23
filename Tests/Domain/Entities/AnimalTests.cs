using KPO_HW2.Domain.Entities;
using KPO_HW2.Domain.Enum;
using KPO_HW2.Domain.Events;
using KPO_HW2.Domain.ValueObject;

namespace Tests.Domain.Entities
{
    public class AnimalTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var species = new Species(AnimalType.Predator, "Lion");
            var name = "Simba";
            var birthDate = DateTime.Now.AddYears(-2);
            var gender = Gender.Male;
            var favoriteFood = new Food(FoodType.Meat, "Beef");
            var status = HealthStatus.Healthy;
            var enclosureId = Guid.NewGuid();

            // Act
            var animal = new Animal(species, name, birthDate, gender, favoriteFood, status, enclosureId);

            // Assert
            Assert.NotEqual(Guid.Empty, animal.AnimalId);
            Assert.Equal(species, animal.Species);
            Assert.Equal(name, animal.Name);
            Assert.Equal(birthDate, animal.BirthDate);
            Assert.Equal(gender, animal.Gender);
            Assert.Equal(favoriteFood, animal.FavoriteFood);
            Assert.Equal(status, animal.Status);
            Assert.Equal(enclosureId, animal.EnclosureId);
            Assert.True(animal.IsHungry);
        }

        [Fact]
        public void Feed_ShouldSetIsHungryToFalse()
        {
            // Arrange
            var species = new Species(AnimalType.Predator, "Lion");
            var animal = new Animal(species, "Simba", DateTime.Now.AddYears(-2),
                                 Gender.Male, new Food(FoodType.Meat, "Beef"),
                                 HealthStatus.Healthy, Guid.NewGuid());

            // Act
            animal.Feed();

            // Assert
            Assert.False(animal.IsHungry);
        }

        [Fact]
        public void Treat_ShouldSetStatusToHealthy()
        {
            // Arrange
            var species = new Species(AnimalType.Predator, "Lion");
            var animal = new Animal(species, "Simba", DateTime.Now.AddYears(-2),
                                 Gender.Male, new Food(FoodType.Meat, "Beef"),
                                 HealthStatus.Sick, Guid.NewGuid());

            // Act
            animal.Treat();

            // Assert
            Assert.Equal(HealthStatus.Healthy, animal.Status);
        }

        [Fact]
        public void MoveToEnclosure_ShouldUpdateEnclosureIdAndAddDomainEvent()
        {
            // Arrange
            var oldEnclosureId = Guid.NewGuid();
            var newEnclosureId = Guid.NewGuid();
            var animal = new Animal(new Species(AnimalType.Predator, "Lion"), "Simba", DateTime.Now.AddYears(-2),
                                 Gender.Male, new Food(FoodType.Meat, "Beef"),
                                 HealthStatus.Healthy, oldEnclosureId);

            // Act
            animal.MoveToEnclosure(newEnclosureId);

            // Assert
            Assert.Equal(newEnclosureId, animal.EnclosureId);
            Assert.Single(animal.DomainEvents);
            Assert.IsType<AnimalMovedEvent>(animal.DomainEvents.First());
        }

        [Fact]
        public void MoveToEnclosure_FromNull_ShouldWorkCorrectly()
        {
            // Arrange
            var newEnclosureId = Guid.NewGuid();
            var animal = new Animal(new Species(AnimalType.Predator, "Lion"), "Simba", DateTime.Now.AddYears(-2),
                                 Gender.Male, new Food(FoodType.Meat, "Beef"),
                                 HealthStatus.Healthy, null);

            // Act
            animal.MoveToEnclosure(newEnclosureId);

            // Assert
            Assert.Equal(newEnclosureId, animal.EnclosureId);
            Assert.Single(animal.DomainEvents);
        }
    }
}
