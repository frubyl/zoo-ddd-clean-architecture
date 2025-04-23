using KPO_HW2.Domain.Entities;
using KPO_HW2.Domain.Enum;
using KPO_HW2.Domain.Repositories;
using KPO_HW2.Domain.Service;
using KPO_HW2.Domain.ValueObject;
using KPO_HW2.Presentation.Contracts.Animals;
using KPO_HW2.Presentation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Presentation
{
    public class AnimalControllerTests
    {
        private readonly Mock<IAnimalRepository> _animalRepoMock;
        private readonly Mock<IAnimalTransferService> _transferServiceMock;
        private readonly AnimalController _controller;

        public AnimalControllerTests()
        {
            _animalRepoMock = new Mock<IAnimalRepository>();
            _transferServiceMock = new Mock<IAnimalTransferService>();
            _controller = new AnimalController(_animalRepoMock.Object, _transferServiceMock.Object);
        }

        [Fact]
        public async Task CreateAnimal_WithValidData_ReturnsCreated()
        {
            // Arrange
            var request = new CreateAnimalRequest
            (
                "Simba",
                DateTime.Now.AddYears(-2),
                Gender.Male,
                FoodType.Meat,
                "Beef",
                HealthStatus.Healthy,
                AnimalType.Predator,
                "Lion"

            );

            // Act
            var result = await _controller.CreateAnimal(request);

            // Assert
            var createdResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(201, createdResult.StatusCode);
            _animalRepoMock.Verify(x => x.AddAnimalAsync(It.IsAny<Animal>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetAnimal_WithExistingId_ReturnsAnimal()
        {
            // Arrange
            var animalId = Guid.NewGuid();
            var animal = new Animal(
                new Species(AnimalType.Predator, "Lion"),
                "Simba",
                DateTime.Now.AddYears(-2),
                Gender.Male,
                new Food(FoodType.Meat, "Beef"),
                HealthStatus.Healthy,
                null);

            _animalRepoMock.Setup(x => x.GetAnimalByIdAsync(animalId, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(animal);

            // Act
            var result = await _controller.GetAnimal(animalId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<GetAnimalResponse>(okResult.Value);
            Assert.Equal(animal.Name, response.Name);
        }

        [Fact]
        public async Task TransferAnimal_WithValidData_ReturnsNoContent()
        {
            // Arrange
            var animalId = Guid.NewGuid();
            var request = new TransferAnimalRequest (Guid.NewGuid());

            // Act
            var result = await _controller.TransferAnimal(animalId, request);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _transferServiceMock.Verify(x => x.TransferAnimal(animalId, request.EnclosureId), Times.Once);
        }
    }
}
