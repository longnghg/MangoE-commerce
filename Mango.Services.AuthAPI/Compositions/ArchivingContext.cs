namespace Mango.Services.AuthAPI.Compositions
{
    public class ArchivingContext : IArchivingContext
    {
        public Guid EngagementId { get; }

        public string ContainerCode { get; } 

        public string Geo { get; }
        public ArchivingContext(string geo, string container)
        {
            Geo = geo;
            ContainerCode = container;
        }
    }
}
