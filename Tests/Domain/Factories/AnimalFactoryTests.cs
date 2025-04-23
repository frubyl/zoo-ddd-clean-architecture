using KPO_HW2.Domain.Enum;
using KPO_HW2.Domain.Factories;
using KPO_HW2.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Domain.Factories
{
    public class AnimalFactoryTests
    {
        private readonly AnimalFactory _factory = new AnimalFactory();
        private readonly Food _testFood = new Food(FoodType.Meat, "Beef");
        private readonly DateTime _validBirthDate = DateTime.UtcNow.AddYears(-1);
        private readonly Species _testSpecies = new Species(AnimalType.Predator, "Lion");

        [Fact]
        public void CreateAnimal_ShouldReturnAnimalWithCorrectProperties()
        {
            // Arrange
            var name = "Simba";
            var gender = Gender.Male;
            var status = HealthStatus.Healthy;
            var enclosureId = Guid.NewGuid();

            // Act
            var animal = _factory.CreateAnimal(_testSpecies, name, _validBirthDate, gender, _testFood, status, enclosureId);

            // Assert
            Assert.Equal(_testSpecies, animal.Species);
            Assert.Equal(name, animal.Name);
            Assert.Equal(_validBirthDate, animal.BirthDate);
            Assert.Equal(gender, animal.Gender);
            Assert.Equal(_testFood, animal.FavoriteFood);
            Assert.Equal(status, animal.Status);
            Assert.Equal(enclosureId, animal.EnclosureId);
            Assert.True(animal.IsHungry);
        }

        [Fact]
        public void CreateAnimal_WithEmptyName_ShouldThrowArgumentException()
        {
            // Arrange
            var emptyName = "";

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                _factory.CreateAnimal(_testSpecies, emptyName, _validBirthDate, Gender.Male, _testFood, HealthStatus.Healthy));
        }

        [Fact]
        public void CreateAnimal_WithFutureBirthDate_ShouldThrowArgumentException()
        {
            // Arrange
            var futureDate = DateTime.UtcNow.AddDays(1);

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                _factory.CreateAnimal(_testSpecies, "Simba", futureDate, Gender.Male, _testFood, HealthStatus.Healthy));
        }

        [Fact]
        public void CreateAnimal_WithNullEnclosureId_ShouldCreateAnimal()
        {
            // Act
            var animal = _factory.CreateAnimal(_testSpecies, "Simba", _validBirthDate, Gender.Male, _testFood, HealthStatus.Healthy, null);

            // Assert
            Assert.Null(animal.EnclosureId);
        }
    }
}
