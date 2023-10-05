namespace Mango.Services.AuthAPI.Compositions
{
    public interface IArchivingProcess
    {
        Task ExecuteAsync(IArchivingContext archivingContext);

    }
}
