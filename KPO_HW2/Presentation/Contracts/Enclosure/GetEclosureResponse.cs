using KPO_HW2.Domain.Entities;
using KPO_HW2.Domain.Enum;
namespace KPO_HW2.Presentation.Contracts.Enclosure
{

    public record GetEnclosureResponse(
        Guid EnclosureId,
        AnimalType Type,
        double Length,
        double Width,
        double Height,
        int CurrentAnimalCount,
        int MaxCapacity,
        bool IsClear);
}
