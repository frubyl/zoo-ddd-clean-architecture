using KPO_HW2.Application.Services;
using KPO_HW2.Domain.Entities;
using KPO_HW2.Domain.Enum;
using KPO_HW2.Domain.Repositories;
using KPO_HW2.Domain.ValueObject;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Application.Services
{
    public class AnimalTransferServiceTests
    {
        private readonly Mock<IAnimalRepository> _animalRepoMock;
        private readonly Mock<IEnclosureRepository> _enclosureRepoMock;
        private readonly AnimalTransferService _service;
        private readonly Food _testFood = new Food(FoodType.Meat, "Beef");
        private readonly Species _testSpecies = new Species(AnimalType.Predator, "Lion");
        public AnimalTransferServiceTests()
        {
            _animalRepoMock = new Mock<IAnimalRepository>();
            _enclosureRepoMock = new Mock<IEnclosureRepository>();
            _service = new AnimalTransferService(_animalRepoMock.Object, _enclosureRepoMock.Object);
        }

        [Fact]
        public async Task TransferAnimal_WithValidData_ShouldTransferAnimal()
        {
            // Arrange
            var animalId = Guid.NewGuid();
            var enclosureId = Guid.NewGuid();
            var animal = new Animal(_testSpecies, "Simba", DateTime.Now.AddYears(-2),
                         Gender.Male, _testFood, HealthStatus.Healthy, null);
            var enclosure = new Enclosure(AnimalType.Predator, new Size(10, 10, 5), 5);

            _animalRepoMock.Setup(x => x.GetAnimalByIdAsync(animalId, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(animal);
            _enclosureRepoMock.Setup(x => x.GetEnclosureByIdAsync(enclosureId, It.IsAny<CancellationToken>()))
                             .ReturnsAsync(enclosure);

            // Act
            await _service.TransferAnimal(animalId, enclosureId);

            // Assert
            _animalRepoMock.Verify(x => x.UpdateAnimalEnclosureAsync(animal, It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(enclosureId, animal.EnclosureId);
        }

        [Fact]
        public async Task TransferAnimal_WithFullEnclosure_ShouldThrowException()
        {
            // Arrange
            var animalId = Guid.NewGuid();
            var enclosureId = Guid.NewGuid();
            var animal = new Animal(_testSpecies, "Simba", DateTime.Now.AddYears(-2),
                         Gender.Male, _testFood, HealthStatus.Healthy, null);
            var enclosure = new Enclosure(AnimalType.Predator, new Size(10, 10, 5), 1);
            enclosure.AddAnimal(Guid.NewGuid()); 

            _animalRepoMock.Setup(x => x.GetAnimalByIdAsync(animalId, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(animal);
            _enclosureRepoMock.Setup(x => x.GetEnclosureByIdAsync(enclosureId, It.IsAny<CancellationToken>()))
                             .ReturnsAsync(enclosure);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _service.TransferAnimal(animalId, enclosureId));
        }

        [Fact]
        public async Task TransferAnimal_WithWrongEnclosureType_ShouldThrowException()
        {
            // Arrange
            var animalId = Guid.NewGuid();
            var enclosureId = Guid.NewGuid();
            var animal = new Animal(_testSpecies, "Simba", DateTime.Now.AddYears(-2),
                         Gender.Male, _testFood, HealthStatus.Healthy, null);
            var enclosure = new Enclosure(AnimalType.Aquatic, new Size(10, 10, 5), 5);

            _animalRepoMock.Setup(x => x.GetAnimalByIdAsync(animalId, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(animal);
            _enclosureRepoMock.Setup(x => x.GetEnclosureByIdAsync(enclosureId, It.IsAny<CancellationToken>()))
                             .ReturnsAsync(enclosure);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _service.TransferAnimal(animalId, enclosureId));
        }
    }
}
