using KPO_HW2.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace KPO_HW2.Presentation.Contracts.Enclosure
{
    public record CreateEnclosureRequest(
        [param: Required]
        [property: JsonConverter(typeof(JsonStringEnumConverter))]
        AnimalType AnimalType,

        [param: Required]
        double Length,

        [param: Required]
        double Width,

        [param: Required]
        double Height,

        [param: Required]
        int MaxCapacity);
}