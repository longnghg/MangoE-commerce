namespace Mango.Services.AuthAPI.Compositions.ab
{
    public class InitializePackagingB : IOperation<IArchivingContext>
    {
        public async Task<IExecutionResult> ExecuteAsync(IArchivingContext context)
        {
            var a = 123;
            var a1 = 123;
            var a2 = 123;
            return   ExecutionResult.DoneSuccessfully(nameof(InitializePackagingB));
        }
    }
}
