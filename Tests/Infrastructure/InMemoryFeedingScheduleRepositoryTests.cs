using KPO_HW2.Domain.Entities;
using KPO_HW2.Domain.Enum;
using KPO_HW2.Infrastructure.Repositories;


namespace Tests.Infrastructure
{
    public class InMemoryFeedingScheduleRepositoryTests
    {
        private readonly InMemoryFeedingScheduleRepository _repository = new InMemoryFeedingScheduleRepository();

        [Fact]
        public async Task AddFeedingScheduleAsync_ShouldAddScheduleToCollection()
        {
            // Arrange
            var schedule = new FeedingSchedule(Guid.NewGuid(), DateTime.Now.AddHours(1), FoodType.Meat);

            // Act
            await _repository.AddFeedingScheduleAsync(schedule);

            // Assert
            var result = await _repository.GetFeedingScheduleByIdAsync(schedule.FeedingScheduleId);
            Assert.Equal(schedule, result);
        }

        [Fact]
        public async Task MarkFeedingAsCompletedAsync_ShouldUpdateCompletionStatus()
        {
            // Arrange
            var schedule = new FeedingSchedule(Guid.NewGuid(), DateTime.Now.AddHours(1), FoodType.Meat);
            await _repository.AddFeedingScheduleAsync(schedule);

            // Act
            await _repository.MarkFeedingAsCompletedAsync(schedule.FeedingScheduleId);

            // Assert
            var updatedSchedule = await _repository.GetFeedingScheduleByIdAsync(schedule.FeedingScheduleId);
            Assert.True(updatedSchedule.IsCompleted);
        }

        [Fact]
        public async Task GetAnimalsIdWithDueFeedingsAsync_ShouldReturnCorrectIds()
        {
            // Arrange
            var animalId1 = Guid.NewGuid();
            var animalId2 = Guid.NewGuid();

            var dueSchedule1 = new FeedingSchedule(animalId1, DateTime.Now.AddHours(-1), FoodType.Meat);
            var dueSchedule2 = new FeedingSchedule(animalId1, DateTime.Now.AddHours(-2), FoodType.Fish);
            var notDueSchedule = new FeedingSchedule(animalId2, DateTime.Now.AddHours(1), FoodType.Fruit);

            await _repository.AddFeedingScheduleAsync(dueSchedule1);
            await _repository.AddFeedingScheduleAsync(dueSchedule2);
            await _repository.AddFeedingScheduleAsync(notDueSchedule);

            // Act
            var result = await _repository.GetAnimalsIdWithDueFeedingsAsync(DateTime.Now);

            // Assert
            Assert.Single(result); // Only animalId1 has due feedings
            Assert.Equal(animalId1, result[0]);
        }

        [Fact]
        public async Task GetCompletedFeedingsCountAsync_ShouldReturnCorrectCount()
        {
            // Arrange
            var completedSchedule = new FeedingSchedule(Guid.NewGuid(), DateTime.Now.AddHours(-1), FoodType.Meat);
            completedSchedule.MarkedAsCompleted();

            var notCompletedSchedule = new FeedingSchedule(Guid.NewGuid(), DateTime.Now.AddHours(1), FoodType.Fish);

            await _repository.AddFeedingScheduleAsync(completedSchedule);
            await _repository.AddFeedingScheduleAsync(notCompletedSchedule);

            // Act
            var count = await _repository.GetCompletedFeedingsCountAsync();

            // Assert
            Assert.Equal(1, count);
        }
    }
}
