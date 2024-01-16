using Azure;
using Mango.Services.PaymentAPI.Services.IAction;
using Mango.Services.PaymentAPI.Services.StepServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using name = System.Net.WebSockets;

namespace Mango.Services.PaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IOrderProcess _orderProcess;
        public PaymentController(IOrderProcess orderProcess)
        {
            _orderProcess = orderProcess;
        }
        [HttpGet]
        public object Get()
        {
          
            _orderProcess.ExecuteProcessAsync();

            return 123;
        }
    }
}
