namespace KPO_HW2.Domain.Service
{
    public interface IFeedingOrganizationService
    {
        public Task ProcessDueFeedingsAsync(CancellationToken cancellationToken);
    }
}
