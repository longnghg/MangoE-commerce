using Mango.Response.ExecutionResult;
using Mango.Response.ExecutionResult.Interfaces;
using Mango.Services.PaymentAPI.Services.IServices;

namespace Mango.Services.PaymentAPI.Services.StepServices
{
    public class PaymentService : IOrderService
    {
        public async Task<IExecutionOperationResult> ExecuteAsync()
        {
            Console.WriteLine(nameof(PaymentService) + " throw message");

            var executeResult = new ExecutionOperationResult(nameof(PaymentService));
            return executeResult;
        }
    }
}
