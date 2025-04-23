using KPO_HW2.Domain.Entities;
using KPO_HW2.Domain.Enum;
using KPO_HW2.Domain.Repositories;
using KPO_HW2.Presentation.Contracts.Enclosure;
using KPO_HW2.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using KPO_HW2.Domain.ValueObject;

namespace Tests.Presentation
{
    public class EnclosureControllerTests
    {
        private readonly Mock<IEnclosureRepository> _repoMock;
        private readonly EnclosureController _controller;

        public EnclosureControllerTests()
        {
            _repoMock = new Mock<IEnclosureRepository>();
            _controller = new EnclosureController(_repoMock.Object);
        }

        [Fact]
        public async Task CreateEnclosure_WithValidData_ReturnsCreated()
        {
            // Arrange
            var request = new CreateEnclosureRequest
            (
                AnimalType.Predator,
                10,
                10,
                5,
                5
            );

            // Act
            var result = await _controller.CreateEnclosure(request);

            // Assert
            var createdResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(201, createdResult.StatusCode);
            _repoMock.Verify(x => x.AddEnclosureAsync(It.IsAny<Enclosure>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetEnclosure_WithExistingId_ReturnsEnclosure()
        {
            // Arrange
            var enclosure = new Enclosure(
                AnimalType.Predator,
                new Size(10, 10, 5),
                5);

            _repoMock.Setup(x => x.GetEnclosureByIdAsync(enclosure.EnclosureId, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(enclosure);

            // Act
            var result = await _controller.GetEnclosure(enclosure.EnclosureId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<GetEnclosureResponse>(okResult.Value);
            Assert.Equal(enclosure.EnclosureId, response.EnclosureId);
        }

        [Fact]
        public async Task DeleteEnclosure_WithExistingId_ReturnsNoContent()
        {
            // Arrange
            var enclosureId = Guid.NewGuid();

            // Act
            var result = await _controller.DeleteEnclosurel(enclosureId);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _repoMock.Verify(x => x.DeleteEnclosureByIdAsync(enclosureId, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
