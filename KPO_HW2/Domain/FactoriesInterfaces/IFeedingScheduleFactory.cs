using KPO_HW2.Domain.Entities;
using KPO_HW2.Domain.Enum;
namespace KPO_HW2.Domain.FactoriesInterfaces
{
    public interface IFeedingScheduleFactory
    {
        public FeedingSchedule CreateFeedingSchedule(
            Guid animalId, 
            DateTime feedingTime, 
            FoodType foodType);
    }
}