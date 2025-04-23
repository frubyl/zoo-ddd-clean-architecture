using KPO_HW2.Application.Services;
using KPO_HW2.Domain.Repositories;
using Moq;

namespace Tests.Application.Services
{
    public class ZooStatisticsServiceTests
    {
        private readonly Mock<IAnimalRepository> _animalRepoMock;
        private readonly Mock<IEnclosureRepository> _enclosureRepoMock;
        private readonly Mock<IFeedingScheduleRepository> _feedingRepoMock;
        private readonly ZooStatisticsService _service;

        public ZooStatisticsServiceTests()
        {
            _animalRepoMock = new Mock<IAnimalRepository>();
            _enclosureRepoMock = new Mock<IEnclosureRepository>();
            _feedingRepoMock = new Mock<IFeedingScheduleRepository>();
            _service = new ZooStatisticsService(_animalRepoMock.Object, _enclosureRepoMock.Object, _feedingRepoMock.Object);
        }

        [Fact]
        public async Task GetAnimalsCount_ShouldReturnCorrectCount()
        {
            // Arrange
            var expectedCount = 5;
            _animalRepoMock.Setup(x => x.GetAnimalsCountAsync(It.IsAny<CancellationToken>()))
                         .ReturnsAsync(expectedCount);

            // Act
            var result = await _service.GetAnimalsCount();

            // Assert
            Assert.Equal(expectedCount, result);
        }

        [Fact]
        public async Task GetFreeEnclosuresCount_ShouldReturnCorrectCount()
        {
            // Arrange
            var expectedCount = 3;
            _enclosureRepoMock.Setup(x => x.GetFreeEnclosureCountAsync(It.IsAny<CancellationToken>()))
                            .ReturnsAsync(expectedCount);

            // Act
            var result = await _service.GetFreeEnclosuresCount();

            // Assert
            Assert.Equal(expectedCount, result);
        }

        [Fact]
        public async Task GetCompletedFeedingsCount_ShouldReturnCorrectCount()
        {
            // Arrange
            var expectedCount = 10;
            _feedingRepoMock.Setup(x => x.GetCompletedFeedingsCountAsync(It.IsAny<CancellationToken>()))
                          .ReturnsAsync(expectedCount);

            // Act
            var result = await _service.GetCompletedFeedingsCount();

            // Assert
            Assert.Equal(expectedCount, result);
        }
    }
}
