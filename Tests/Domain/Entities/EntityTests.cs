
using KPO_HW2.Domain.Entities;
using MediatR;
using Moq;

namespace Tests.Domain.Entities
{
    public class EntityTests
    {
        [Fact]
        public void AddDomainEvent_ShouldAddEventToList()
        {
            // Arrange
            var entity = new TestEntity();
            var eventMock = new Mock<INotification>();

            // Act
            entity.AddDomainEvent(eventMock.Object);

            // Assert
            Assert.Single(entity.DomainEvents);
            Assert.Equal(eventMock.Object, entity.DomainEvents.First());
        }

        [Fact]
        public void RemoveDomainEvent_ShouldRemoveEventFromList()
        {
            // Arrange
            var entity = new TestEntity();
            var eventMock = new Mock<INotification>();
            entity.AddDomainEvent(eventMock.Object);

            // Act
            entity.RemoveDomainEvent(eventMock.Object);

            // Assert
            Assert.Empty(entity.DomainEvents);
        }

        [Fact]
        public void ClearDomainEvents_ShouldClearAllEvents()
        {
            // Arrange
            var entity = new TestEntity();
            entity.AddDomainEvent(new Mock<INotification>().Object);
            entity.AddDomainEvent(new Mock<INotification>().Object);

            // Act
            entity.CLearDomainEvent();

            // Assert
            Assert.Empty(entity.DomainEvents);
        }

        private class TestEntity : Entity { }
    }
}
