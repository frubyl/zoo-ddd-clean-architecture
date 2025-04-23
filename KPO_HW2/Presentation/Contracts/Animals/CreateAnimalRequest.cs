using KPO_HW2.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace KPO_HW2.Presentation.Contracts.Animals
{

    public record CreateAnimalRequest(
        [param: Required]
        string Name,

        [param: Required]
        DateTime BirthDate,

        [param: Required]
        [property: JsonConverter(typeof(JsonStringEnumConverter))]
        Gender Gender,

        [param: Required]
        [property: JsonConverter(typeof(JsonStringEnumConverter))]
        FoodType FoodType,

        [param: Required]
        string FoodName,

        [param: Required]
        [property: JsonConverter(typeof(JsonStringEnumConverter))]
        HealthStatus Status,

        [param: Required]
        [property: JsonConverter(typeof(JsonStringEnumConverter))]
        AnimalType AnimalType,

        [param: Required]
        string SpeciesName);
}