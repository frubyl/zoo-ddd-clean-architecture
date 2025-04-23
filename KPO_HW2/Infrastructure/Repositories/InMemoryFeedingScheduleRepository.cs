
using KPO_HW2.Domain.Entities;
using KPO_HW2.Domain.Repositories;

namespace KPO_HW2.Infrastructure.Repositories
{
    public class InMemoryFeedingScheduleRepository : IFeedingScheduleRepository
    {
        private readonly List<FeedingSchedule> _feedingSchedules = new List<FeedingSchedule>();

        public async Task AddFeedingScheduleAsync(FeedingSchedule feedingSchedule, CancellationToken cancellationToken = default)
        {
            _feedingSchedules.Add(feedingSchedule);
        }

        public async Task DeleteFeedingScheduleByIdAsync(Guid feedingScheduleId, CancellationToken cancellationToken = default)
        {
            var removedCount = _feedingSchedules.RemoveAll(fs => fs.FeedingScheduleId == feedingScheduleId);
            if (removedCount == 0)
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<List<Guid>> GetAnimalsIdWithDueFeedingsAsync(DateTime now, CancellationToken cancellationToken = default)
        {
            var result = _feedingSchedules
                .Where(fs => fs.FeedingTime <= now && !fs.IsCompleted)
                .Select(fs => fs.AnimalId)
                .Distinct()
                .ToList();

            return result;
        }

        public async Task<int> GetCompletedFeedingsCountAsync(CancellationToken cancellationToken = default)
        {
            var count = _feedingSchedules.Count(fs => fs.IsCompleted);
            return count;
        }

        public async Task<FeedingSchedule> GetFeedingScheduleByIdAsync(Guid feedingScheduleID, CancellationToken cancellationToken = default)
        {
            var result = _feedingSchedules.FirstOrDefault(fs => fs.FeedingScheduleId == feedingScheduleID);
            if (result == null)
            {
                throw new KeyNotFoundException();
            }
            return result;
        }

        public async Task<List<FeedingSchedule>> GetAllFeedingSchedulesAsync(CancellationToken cancellationToken = default)
        {
            return _feedingSchedules;
        }
        public async Task MarkFeedingAsCompletedAsync(Guid feedingScheduleId, CancellationToken cancellationToken = default)
        {
            var sched = _feedingSchedules.FirstOrDefault(fs => fs.FeedingScheduleId == feedingScheduleId);
            if (sched == null)
            {
                throw new KeyNotFoundException();
            }
            sched.MarkedAsCompleted();
        }

        public async Task<List<FeedingSchedule>> GetSchedulesByAnimalIdAsync(Guid animalId, CancellationToken cancellationToken = default)
        {
            var result = _feedingSchedules
                .Where(fs => fs.AnimalId == animalId)
                .ToList();

            return result;
        }
        public InMemoryFeedingScheduleRepository() { }
    }
}
