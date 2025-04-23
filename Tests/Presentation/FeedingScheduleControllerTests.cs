using KPO_HW2.Domain.Entities;
using KPO_HW2.Domain.Enum;
using KPO_HW2.Domain.Repositories;
using KPO_HW2.Domain.ValueObject;
using KPO_HW2.Presentation.Contracts.FeedingSchedule;
using KPO_HW2.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace Tests.Presentation
{
    public class FeedingScheduleControllerTests
    {
        private readonly Mock<IFeedingScheduleRepository> _scheduleRepoMock;
        private readonly Mock<IAnimalRepository> _animalRepoMock;
        private readonly FeedingScheduleController _controller;

        public FeedingScheduleControllerTests()
        {
            _scheduleRepoMock = new Mock<IFeedingScheduleRepository>();
            _animalRepoMock = new Mock<IAnimalRepository>();
            _controller = new FeedingScheduleController(_scheduleRepoMock.Object, _animalRepoMock.Object);
        }

        [Fact]
        public async Task CreateFeedingSchedule_WithValidData_ReturnsCreated()
        {
            // Arrange
            var request = new CreateFeedingScheduleRequest
            (
                Guid.NewGuid(),
                DateTime.Now.AddHours(1),
                FoodType.Meat
            );

            _animalRepoMock.Setup(x => x.GetAnimalByIdAsync(request.AnimalId, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(new Animal(
                              new Species(AnimalType.Predator, "Lion"),
                              "Simba",
                              DateTime.Now.AddYears(-2),
                              Gender.Male,
                              new Food(FoodType.Meat, "Beef"),
                              HealthStatus.Healthy,
                              null));

            // Act
            var result = await _controller.CreateFeedingSchedule(request);

            // Assert
            var createdResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(201, createdResult.StatusCode);
            _scheduleRepoMock.Verify(x => x.AddFeedingScheduleAsync(It.IsAny<FeedingSchedule>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetFeedingSchedulesByAnimal_WithExistingAnimal_ReturnsSchedules()
        {
            // Arrange
            var animalId = Guid.NewGuid();
            var schedules = new List<FeedingSchedule>
            {
                new FeedingSchedule(animalId, DateTime.Now.AddHours(1), FoodType.Meat),
                new FeedingSchedule(animalId, DateTime.Now.AddHours(2), FoodType.Fish)
            };

            _animalRepoMock.Setup(x => x.GetAnimalByIdAsync(animalId, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(new Animal(
                              new Species(AnimalType.Predator, "Lion"),
                              "Simba",
                              DateTime.Now.AddYears(-2),
                              Gender.Male,
                              new Food(FoodType.Meat, "Beef"),
                              HealthStatus.Healthy,
                              null));

            _scheduleRepoMock.Setup(x => x.GetSchedulesByAnimalIdAsync(animalId, It.IsAny<CancellationToken>()))
                            .ReturnsAsync(schedules);

            // Act
            var result = await _controller.GetFeedingSchedulesByAnimal(animalId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsAssignableFrom<IEnumerable<GetFeedingScheduleResponse>>(okResult.Value);
            Assert.Equal(2, response.Count());
        }
    }
}
