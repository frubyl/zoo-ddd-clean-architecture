using KPO_HW2.Domain.Entities;
using KPO_HW2.Domain.Enum;
using KPO_HW2.Domain.FactoriesInterfaces;

namespace KPO_HW2.Domain.Factories
{

    public class FeedingScheduleFactory : IFeedingScheduleFactory
    {
        public FeedingSchedule CreateFeedingSchedule(
           Guid animalId,
           DateTime feedingTime,
           FoodType foodType)
        {
            if (animalId == Guid.Empty)
            {
                throw new ArgumentException("ID животного не может быть пустым.", nameof(animalId));
            }
            return new FeedingSchedule(animalId, feedingTime, foodType);
        }
    }
}