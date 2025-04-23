using KPO_HW2.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace KPO_HW2.Presentation.Contracts.FeedingSchedule
{
    public record CreateFeedingScheduleRequest(
        [param: Required] Guid AnimalId,
        [param: Required] DateTime FeedingTime,
        [param: Required] FoodType FoodType);

}