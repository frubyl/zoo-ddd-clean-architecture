using KPO_HW2.Domain.Enum;
using KPO_HW2.Domain.Events;

namespace KPO_HW2.Domain.Entities
{
    public class FeedingSchedule 
    {
        public Guid FeedingScheduleId { get; init; }

        public Guid AnimalId { get; private set; }
        public DateTime FeedingTime { get; private set; }
        public FoodType FoodType { get; private set; }
        public bool IsCompleted { get; private set; }

        public void ChangeFoodType(FoodType foodType) { 
            FoodType = foodType; 
        }

        public void ChangeFeedingType(DateTime newFeedingTime)
        {
            FeedingTime = newFeedingTime;
        }

        public void MarkedAsCompleted()
        {
            IsCompleted = true;
        }

        public FeedingSchedule(Guid animalId, DateTime feedingTime, FoodType foodType)
        {
            FeedingScheduleId = Guid.NewGuid();
            AnimalId = animalId;
            FeedingTime = feedingTime;
            FoodType = foodType;
        }
    }
}
