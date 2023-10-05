using Autofac;

namespace Mango.Services.AuthAPI.Compositions
{
    public static class CompositionRoot
    {

        public static void LongBuild(this ContainerBuilder container)
        {
            container.RegisterModule<TestingModule>();
        }
    }
}
