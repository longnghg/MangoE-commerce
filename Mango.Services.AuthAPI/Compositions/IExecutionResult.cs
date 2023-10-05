namespace Mango.Services.AuthAPI.Compositions
{
    public interface IExecutionResult
    {
        string OperationName { get; }
        int? ArchiveFailedCode { get; }
        bool IsSucceed { get; }
        Exception Error { get; }
    }
}
