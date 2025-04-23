using KPO_HW2.Domain.Enum;
namespace KPO_HW2.Presentation.Contracts.Animals
{

public record GetAnimalResponse(
    Guid AnimalId,
    string Name,
    AnimalType AnimalType,
    string SpeciesName,
    DateTime BirthDate,
    Gender Gender,
    FoodType FoodType,
    string FoodName,
    HealthStatus Status,
    Guid? EnclosureId,
    bool IsHungry);
}