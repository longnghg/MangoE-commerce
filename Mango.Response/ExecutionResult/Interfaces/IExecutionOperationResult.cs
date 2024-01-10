using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Response.ExecutionResult.Interfaces
{
    public interface IExecutionOperationResult
    {
        string OperationName { get; }
        bool IsSucceed { get; }
        Exception Error { get; }
    }
}
