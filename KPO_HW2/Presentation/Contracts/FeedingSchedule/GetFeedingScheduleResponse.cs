using KPO_HW2.Domain.Enum;
namespace KPO_HW2.Presentation.Contracts.FeedingSchedule
{
    public record GetFeedingScheduleResponse(
        Guid FeedingScheduleId,
        Guid AnimalId,
        DateTime FeedingTime,
        FoodType FoodType,
        bool IsCompleted);
}