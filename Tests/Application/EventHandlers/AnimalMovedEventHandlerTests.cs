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
    public class AnimalMovedEventHandlerTests
    {
        private readonly Mock<IEnclosureRepository> _enclosureRepoMock;
        private readonly AnimalMovedEventHandler _handler;

        public AnimalMovedEventHandlerTests()
        {
            _enclosureRepoMock = new Mock<IEnclosureRepository>();
            _handler = new AnimalMovedEventHandler(_enclosureRepoMock.Object);
        }

        [Fact]
        public async Task Handle_WithFromEnclosure_ShouldCallRemoveAndAdd()
        {
            // Arrange
            var animalId = Guid.NewGuid();
            var fromEnclosureId = Guid.NewGuid();
            var toEnclosureId = Guid.NewGuid();
            var @event = new AnimalMovedEvent(animalId, fromEnclosureId, toEnclosureId);

            // Act
            await _handler.Handle(@event, CancellationToken.None);

            // Assert
    _enclosureRepoMock.Verify(x => x.RemoveAnimalFromEnclosureAsync(animalId, fromEnclosureId, It.IsAny<CancellationToken>()), Times.Once);
    _enclosureRepoMock.Verify(x => x.AddAnimalToEnclosureAsync(animalId, toEnclosureId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_WithoutFromEnclosure_ShouldOnlyCallAdd()
        {
            // Arrange
            var animalId = Guid.NewGuid();
            var toEnclosureId = Guid.NewGuid();
            var @event = new AnimalMovedEvent(animalId, null, toEnclosureId);

            // Act
            await _handler.Handle(@event, CancellationToken.None);

            // Assert
            _enclosureRepoMock.Verify(x => x.RemoveAnimalFromEnclosureAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
            _enclosureRepoMock.Verify(x => x.AddAnimalToEnclosureAsync(animalId, toEnclosureId, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
