using Azure;

namespace Mango.Services.AuthAPI.Compositions
{
    public class ArchivingProcess : IArchivingProcess
    {

        IOperation<IArchivingContext>[] _archiveOperations;

        public ArchivingProcess(IOperation<IArchivingContext>[] archiveOperations)
        {
            _archiveOperations = archiveOperations;
        }
        public async Task ExecuteAsync(IArchivingContext archivingContext)
        {
            foreach (var operation in _archiveOperations)
            {
                var executionResult = await operation.ExecuteAsync(archivingContext);

                if (!executionResult.IsSucceed)
                {
                    return;
                }
            }
        }
    }
}
