
using KPO_HW2.Domain.Repositories;
using KPO_HW2.Domain.Service;

namespace KPO_HW2.Application.Services
{
    public class ZooStatisticsService : IZooStatisticsService
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IEnclosureRepository _enclosureRepository;
        private readonly IFeedingScheduleRepository _feedingScheduleRepository;
        public async Task<int> GetAnimalsCount()
        {
            int animalsCount = await _animalRepository.GetAnimalsCountAsync();
            return animalsCount;
        }
        public async Task<int> GetFreeEnclosuresCount()
        {
            int freeEnclosuresCount = await _enclosureRepository.GetFreeEnclosureCountAsync();
            return freeEnclosuresCount;
        }
        public async Task<int> GetCompletedFeedingsCount()
        {
            int completedFeedingsCount = await _feedingScheduleRepository.GetCompletedFeedingsCountAsync();
            return completedFeedingsCount;
        }
        public ZooStatisticsService(IAnimalRepository animalRepository, IEnclosureRepository enclosureRepository, IFeedingScheduleRepository feedingScheduleRepository)
        {
            _animalRepository = animalRepository;
            _enclosureRepository = enclosureRepository;
            _feedingScheduleRepository = feedingScheduleRepository;
        }
    }
}
