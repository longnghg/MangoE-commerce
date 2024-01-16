using Mango.Response.ExecutionResult.Interfaces;

namespace Mango.Services.PaymentAPI.Services.IServices
{
    public interface IOrderService
    {
        Task<IExecutionOperationResult> ExecuteAsync();
    }
}
    