using KPO_HW2.Application.EventHandlers;
using KPO_HW2.Domain.Events;
using KPO_HW2.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Application.EventHandlers
{
    public class FeedingTimeEventHandlerTests
    {
        private readonly Mock<IAnimalRepository> _animalRepoMock;
        private readonly FeedingTimeEventHandler _handler;

        public FeedingTimeEventHandlerTests()
        {
            _animalRepoMock = new Mock<IAnimalRepository>();
            _handler = new FeedingTimeEventHandler(_animalRepoMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCallChangeIsHungryWithFalse()
        {
            // Arrange
            var animalId = Guid.NewGuid();
            var @event = new FeedingTimeEvent(animalId);


            // Act
            await _handler.Handle(@event, CancellationToken.None);

            // Assert
            _animalRepoMock.Verify(x => x.ChangeIsHungryAsync(animalId, false, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
