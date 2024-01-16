using Mango.Response.ExecutionResult;
using Mango.Response.ExecutionResult.Interfaces;
using Mango.Services.PaymentAPI.Services.IAction;
using Mango.Services.PaymentAPI.Services.IServices;
using Mango.Services.PaymentAPI.Services.StepServices.StepEnums;
using Microsoft.EntityFrameworkCore.Storage;

namespace Mango.Services.PaymentAPI.Services.Action
{
    public class OrderProcess : IOrderProcess
    {
        private readonly Func<OrderProcessStep, IOrderService> _orderService;
        public OrderProcess(Func<OrderProcessStep, IOrderService> orderService)
        {
            _orderService = orderService;
        }
        public async Task<IExecutionOperationResult> ExecuteProcessAsync()
        {
            // loop each EnumStep to Execute
            foreach (OrderProcessStep item in Enum.GetValues(typeof(OrderProcessStep)))
            {
                var executionResult = await _orderService(item).ExecuteAsync();
                if (!executionResult.IsSucceed)
                {
                    return new ExecutionOperationResult($"{nameof(OrderProcess)}", executionResult.IsSucceed, executionResult.Error);
                }
            }
            return new ExecutionOperationResult($"{nameof(OrderProcess)}");
        }
    }
}
