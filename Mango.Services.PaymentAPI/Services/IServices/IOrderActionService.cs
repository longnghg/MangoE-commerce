using Mango.Response.ExecutionResult.Interfaces;

namespace Mango.Services.PaymentAPI.Services.IServices
{
    public interface IOrderActionService
    {
        Task<IExecutionOperationResult> ExecuteAsync();
    }
}
