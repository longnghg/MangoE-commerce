namespace Mango.Services.AuthAPI.Compositions
{
    public interface IArchivingContext
    {
        Guid EngagementId { get; }

        string ContainerCode { get; }

        string Geo { get; }
    }
}