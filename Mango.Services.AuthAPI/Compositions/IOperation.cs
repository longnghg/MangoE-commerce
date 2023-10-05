using Mango.Services.AuthAPI.Compositions;
using System.Threading.Tasks;

namespace Mango.Services.AuthAPI.Compositions
{
    public interface IOperation<in TContext>
    {
        Task<IExecutionResult> ExecuteAsync(TContext context);
    }
}
