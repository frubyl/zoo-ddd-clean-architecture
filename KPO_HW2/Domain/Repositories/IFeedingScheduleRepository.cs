using KPO_HW2.Domain.Entities;

namespace KPO_HW2.Domain.Repositories
{
    public interface IFeedingScheduleRepository
    {
        public Task AddFeedingScheduleAsync(FeedingSchedule feedingSchedule, CancellationToken cancellationToken = default);

        public Task DeleteFeedingScheduleByIdAsync(Guid feedingScheduleId, CancellationToken cancellationToken = default);

        public Task<List<Guid>> GetAnimalsIdWithDueFeedingsAsync(DateTime now, CancellationToken cancellationToken = default);

        public Task<int> GetCompletedFeedingsCountAsync(CancellationToken cancellationToken = default);

        public Task<FeedingSchedule> GetFeedingScheduleByIdAsync(Guid feedingScheduleID, CancellationToken cancellationToken = default);

        public Task<List<FeedingSchedule>> GetAllFeedingSchedulesAsync(CancellationToken cancellationToken = default);

        public Task<List<FeedingSchedule>> GetSchedulesByAnimalIdAsync(Guid animalId, CancellationToken cancellationToken = default);
        public Task MarkFeedingAsCompletedAsync(Guid feedingScheduleId, CancellationToken cancellationToken = default);

    }
}
