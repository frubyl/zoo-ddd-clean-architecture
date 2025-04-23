using KPO_HW2.Domain.Entities;
using KPO_HW2.Domain.Enum;
using KPO_HW2.Domain.ValueObject;
using KPO_HW2.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;


namespace Tests.Infrastructure
{
    public class InMemoryAnimalRepositoryTests
    {
        private readonly InMemoryAnimalRepository _repository;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IServiceScopeFactory> _scopeFactoryMock;

        public InMemoryAnimalRepositoryTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _scopeFactoryMock = new Mock<IServiceScopeFactory>();

            var serviceScopeMock = new Mock<IServiceScope>();
            var serviceProviderMock = new Mock<IServiceProvider>();

            serviceProviderMock.Setup(x => x.GetService(typeof(IMediator)))
                             .Returns(_mediatorMock.Object);
            serviceScopeMock.Setup(x => x.ServiceProvider)
                          .Returns(serviceProviderMock.Object);
            _scopeFactoryMock.Setup(x => x.CreateScope())
                            .Returns(serviceScopeMock.Object);

            _repository = new InMemoryAnimalRepository(_scopeFactoryMock.Object);
        }

        [Fact]
        public async Task AddAnimalAsync_ShouldAddAnimalToCollection()
        {
            // Arrange
            var animal = new Animal(new Species(AnimalType.Predator, "Lion"), "Simba", DateTime.Now.AddYears(-2),
                Gender.Male, new Food(FoodType.Meat, "Beef"), HealthStatus.Healthy, null);

            // Act
            await _repository.AddAnimalAsync(animal);

            // Assert
            var result = await _repository.GetAnimalByIdAsync(animal.AnimalId);
            Assert.Equal(animal, result);
        }

        [Fact]
        public async Task DeleteAnimalByIdAsync_ShouldRemoveAnimal()
        {
            // Arrange
            var animal = new Animal(new Species(AnimalType.Predator, "Lion"), "Simba", DateTime.Now.AddYears(-2),
                Gender.Male, new Food(FoodType.Meat, "Beef"), HealthStatus.Healthy, null);
            await _repository.AddAnimalAsync(animal);

            // Act
            await _repository.DeleteAnimalByIdAsync(animal.AnimalId);

            // Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _repository.GetAnimalByIdAsync(animal.AnimalId));
        }

        [Fact]
        public async Task UpdateAnimalEnclosureAsync_ShouldUpdateAndDispatchEvents()
        {
            // Arrange
            var animal = new Animal(new Species(AnimalType.Predator, "Lion"), "Simba", DateTime.Now.AddYears(-2),
                Gender.Male, new Food(FoodType.Meat, "Beef"), HealthStatus.Healthy, null);
            var newEnclosureId = Guid.NewGuid();
            await _repository.AddAnimalAsync(animal);

            animal.MoveToEnclosure(newEnclosureId);

            // Act
            await _repository.UpdateAnimalEnclosureAsync(animal);

            // Assert
            var updatedAnimal = await _repository.GetAnimalByIdAsync(animal.AnimalId);
            Assert.Equal(newEnclosureId, updatedAnimal.EnclosureId);
            _mediatorMock.Verify(x => x.Publish(It.IsAny<INotification>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ChangeIsHungryAsync_ShouldUpdateHungerStatus()
        {
            // Arrange
            var animal = new Animal(new Species(AnimalType.Predator, "Lion"), "Simba", DateTime.Now.AddYears(-2),
                Gender.Male, new Food(FoodType.Meat, "Beef"), HealthStatus.Healthy, null);
            await _repository.AddAnimalAsync(animal);

            // Act
            await _repository.ChangeIsHungryAsync(animal.AnimalId, false);

            // Assert
            var result = await _repository.GetAnimalByIdAsync(animal.AnimalId);
            Assert.False(result.IsHungry);
        }
    }
}
