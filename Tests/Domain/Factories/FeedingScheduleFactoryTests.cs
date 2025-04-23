using KPO_HW2.Domain.Enum;
using KPO_HW2.Domain.Factories;


namespace Tests.Domain.Factories
{
    public class FeedingScheduleFactoryTests
    {
        private readonly FeedingScheduleFactory _factory = new FeedingScheduleFactory();
        private readonly DateTime _validFeedingTime = DateTime.Now.AddHours(1);

        [Fact]
        public void CreateFeedingSchedule_ShouldReturnScheduleWithCorrectProperties()
        {
            // Arrange
            var animalId = Guid.NewGuid();
            var foodType = FoodType.Meat;

            // Act
            var schedule = _factory.CreateFeedingSchedule(animalId, _validFeedingTime, foodType);

            // Assert
            Assert.Equal(animalId, schedule.AnimalId);
            Assert.Equal(_validFeedingTime, schedule.FeedingTime);
            Assert.Equal(foodType, schedule.FoodType);
            Assert.False(schedule.IsCompleted);
        }

        [Fact]
        public void CreateFeedingSchedule_WithEmptyAnimalId_ShouldThrowArgumentException()
        {
            // Arrange
            var emptyId = Guid.Empty;

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                _factory.CreateFeedingSchedule(emptyId, _validFeedingTime, FoodType.Meat));
        }
    }
}
