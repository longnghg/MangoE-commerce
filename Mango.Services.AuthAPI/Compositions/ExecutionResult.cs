namespace Mango.Services.AuthAPI.Compositions
{
    public class ExecutionResult : IExecutionResult
    {
        public ExecutionResult(string operationName)
        {
            IsSucceed = true;
            OperationName = operationName;
        }
        public string OperationName { get; }
        public int? ArchiveFailedCode { get; }
        public bool IsSucceed { get; }
        public Exception Error { get; }
        public static ExecutionResult DoneSuccessfully(string operationName)
        {
            return new ExecutionResult(operationName);
        }
    }
}
