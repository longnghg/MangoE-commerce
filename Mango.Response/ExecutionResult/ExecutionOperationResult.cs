using Mango.Response.ExecutionResult.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Response.ExecutionResult
{
    public class ExecutionOperationResult : IExecutionOperationResult
    {
        public string OperationName { get; }
        public bool IsSucceed { get; }
        public Exception Error { get; }

        public ExecutionOperationResult(string operationName, bool isSucceed = true, Exception error = null) 
        {
            
        }
    }
}
