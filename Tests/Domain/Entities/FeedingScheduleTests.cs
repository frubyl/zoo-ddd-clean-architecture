using KPO_HW2.Domain.Entities;
using KPO_HW2.Domain.Enum;

namespace Tests.Domain.Entities
{
    public class FeedingScheduleTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var animalId = Guid.NewGuid();
            var feedingTime = DateTime.Now.AddHours(1);
            var foodType = FoodType.Meat;

            // Act
            var schedule = new FeedingSchedule(animalId, feedingTime, foodType);

            // Assert
            Assert.NotEqual(Guid.Empty, schedule.FeedingScheduleId);
            Assert.Equal(animalId, schedule.AnimalId);
            Assert.Equal(feedingTime, schedule.FeedingTime);
            Assert.Equal(foodType, schedule.FoodType);
            Assert.False(schedule.IsCompleted);
        }

        [Fact]
        public void ChangeFoodType_ShouldUpdateFoodType()
        {
            // Arrange
            var schedule = new FeedingSchedule(Guid.NewGuid(), DateTime.Now, FoodType.Meat);
            var newFoodType = FoodType.Fish;

            // Act
            schedule.ChangeFoodType(newFoodType);

            // Assert
            Assert.Equal(newFoodType, schedule.FoodType);
        }

        [Fact]
        public void ChangeFeedingType_ShouldUpdateFeedingTime()
        {
            // Arrange
            var schedule = new FeedingSchedule(Guid.NewGuid(), DateTime.Now, FoodType.Meat);
            var newFeedingTime = DateTime.Now.AddHours(2);

            // Act
            schedule.ChangeFeedingType(newFeedingTime);

            // Assert
            Assert.Equal(newFeedingTime, schedule.FeedingTime);
        }

        [Fact]
        public void MarkedAsCompleted_ShouldSetIsCompletedToTrue()
        {
            // Arrange
            var schedule = new FeedingSchedule(Guid.NewGuid(), DateTime.Now, FoodType.Meat);

            // Act
            schedule.MarkedAsCompleted();

            // Assert
            Assert.True(schedule.IsCompleted);
        }
    }
}
