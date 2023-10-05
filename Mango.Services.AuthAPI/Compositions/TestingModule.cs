using Autofac;
using Mango.Services.AuthAPI.Compositions.ab;

namespace Mango.Services.AuthAPI.Compositions
{
    public class TestingModule : BaseModule
    {
        protected override void Load(ContainerBuilder builder)
        {

            // Đăng ký key

            builder.RegisterType<InitializePackaging>()
            .As<IOperation<IArchivingContext>>()
            .Keyed<IOperation<IArchivingContext>>(nameof(InitializePackaging));

            builder.RegisterType<InitializePackagingB>()
            .As<IOperation<IArchivingContext>>()
            .Keyed<IOperation<IArchivingContext>>(nameof(InitializePackagingB));


            //Engagement packaging ArchivingProcess registration
            builder.RegisterType<ArchivingProcess>().Named<IArchivingProcess>(nameof(ArchiveKind.EngagementArchive))
                .InstancePerLifetimeScope()
                .WithParameter(GetChain<IOperation<IArchivingContext>>(
                  nameof(InitializePackaging),
                    nameof(InitializePackagingB)
                /*TODO: 2021/09/23 - Not processed yet
                nameof(DeleteSPOFiles),
                nameof(CleanupTempFolder) //TODO: Keep local file for checking data
                TODO END*/

                ));

            //LegalHold packaging ArchivingProcess registration
            builder.RegisterType<ArchivingProcess>().Named<IArchivingProcess>(nameof(ArchiveKind.LegalHold))
                .InstancePerLifetimeScope()
                .WithParameter(GetChain<IOperation<IArchivingContext>>(
                    nameof(InitializePackaging),
                    nameof(InitializePackagingB)
                ));
            


            // đăng ký hàm lựa chọn dịch vụ
            builder.Register<Func<ArchiveKind, IArchivingProcess>>(c =>
            {
                var context = c.Resolve<IComponentContext>();
                return (arhiveKind) =>
                {
                    switch (arhiveKind)
                    {
                        case ArchiveKind.EngagementArchive:
                            return context.ResolveNamed<IArchivingProcess>(nameof(ArchiveKind.EngagementArchive));
                        case ArchiveKind.LegalHold:
                            return context.ResolveNamed<IArchivingProcess>(nameof(ArchiveKind.LegalHold));
                        default:
                            throw new NotSupportedException($"{arhiveKind.ToString()} archive process is not supported.");
                    }
                };
            });

        }


    }
}
