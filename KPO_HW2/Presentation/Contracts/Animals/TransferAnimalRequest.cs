using KPO_HW2.Domain.Enum;
using System.ComponentModel.DataAnnotations;
namespace KPO_HW2.Presentation.Contracts.Animals
{

    public record TransferAnimalRequest(
    [param: Required]
    Guid EnclosureId);
}