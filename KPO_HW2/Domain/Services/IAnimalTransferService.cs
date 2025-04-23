namespace KPO_HW2.Domain.Service
{
    public interface IAnimalTransferService
    {
        public Task TransferAnimal(Guid animalId, Guid enclosureId);
    }
}
