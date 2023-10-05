using Autofac;
using Autofac.Core;

namespace Mango.Services.AuthAPI.Compositions
{
    public class BaseModule : Module
    {

        //// Định nghĩa GetChain giống như mã gốc 
        //public IFileHandler[] GetChain<IFileHandler>(params string[] names)
        //{
        //    IFileHandler[] handlers = new IFileHandler[names.Length];

        //    for (int i = 0; i < names.Length; i++)
        //    {
        //        handlers[i] = ResolveKeyed<IFileHandler>(names[i]);
        //    }

        //    return handlers;
        //}

        protected ResolvedParameter GetChain<TAbstraction>(params string[] test)
        {
            var b = new ResolvedParameter(
                (pi, ctx) => pi.ParameterType == typeof(TAbstraction[]),
                (pi, ctx) =>
                {
                    TAbstraction[] items = new TAbstraction[test.Length];
                    for (int index = 0; index < items.Length; index++)
                    {
                        items[index] = ctx.ResolveKeyed<TAbstraction>(test[index]);
                    }
                    return items;
                });

            return b;
        }
    }
}
