using Mango.Response.ExecutionResult.Interfaces;

namespace Mango.Services.PaymentAPI.Services.IAction
{
    public interface IOrderProcess
    {
        Task<IExecutionOperationResult> ExecuteProcessAsync();
    }
}
