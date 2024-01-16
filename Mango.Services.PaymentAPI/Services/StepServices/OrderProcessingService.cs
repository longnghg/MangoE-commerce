using Mango.Response.ExecutionResult;
using Mango.Response.ExecutionResult.Interfaces;
using Mango.Services.PaymentAPI.Services.IServices;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Mango.Services.PaymentAPI.Services.StepServices
{

    public class OrderProcessingService : IOrderService
    {
        public async Task<IExecutionOperationResult> ExecuteAsync()
        {
            Console.WriteLine(nameof(OrderProcessingService) + " throw message");

            var executeResult = new ExecutionOperationResult(nameof(OrderProcessingService));
            return executeResult;
        }
    }
}
