using KPO_HW2.Application.Services;
using KPO_HW2.Domain.Entities;
using KPO_HW2.Domain.Enum;
using KPO_HW2.Domain.Repositories;
using MediatR;
using Moq;
using KPO_HW2.Domain.Events;

namespace Tests.Application.Services
{
    public class FeedingOrganizationServiceTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IFeedingScheduleRepository> _repoMock;
        private readonly FeedingOrganizationService _service;

        public FeedingOrganizationServiceTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _repoMock = new Mock<IFeedingScheduleRepository>();
            _service = new FeedingOrganizationService(_mediatorMock.Object, _repoMock.Object);
        }

        [Fact]
        public async Task ProcessDueFeedingsAsync_ShouldProcessDueFeedings()
        {
            // Arrange
            var animalId = Guid.NewGuid();
            var dueSchedule = new FeedingSchedule(animalId, DateTime.UtcNow.AddMinutes(-1), FoodType.Meat);
            var notDueSchedule = new FeedingSchedule(animalId, DateTime.UtcNow.AddHours(1), FoodType.Meat);

            _repoMock.Setup(x => x.GetAllFeedingSchedulesAsync(It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new List<FeedingSchedule> { dueSchedule, notDueSchedule });

            // Act
            await _service.ProcessDueFeedingsAsync(CancellationToken.None);

            // Assert
            _mediatorMock.Verify(x => x.Publish(It.Is<FeedingTimeEvent>(e => e.AnimalId == animalId), It.IsAny<CancellationToken>()), Times.Once);
            _repoMock.Verify(x => x.MarkFeedingAsCompletedAsync(dueSchedule.FeedingScheduleId, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
