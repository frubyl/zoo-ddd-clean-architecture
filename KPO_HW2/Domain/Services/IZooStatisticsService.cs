using KPO_HW2.Domain.Entities;

namespace KPO_HW2.Domain.Service
{
    public interface IZooStatisticsService
    {
        public Task<int> GetAnimalsCount();
        public Task<int> GetFreeEnclosuresCount();
        public Task<int> GetCompletedFeedingsCount();
    }
}
