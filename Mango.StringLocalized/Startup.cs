using Mango.StringLocalized.Localizer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using StringLocalize.Localizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.StringLocalized
{
    public static class Startup
    {
        public static IServiceCollection ConfigureLocalizationServices(this IServiceCollection services)
        {
            var appDirectory = AppContext.BaseDirectory;
            IOptions<CustomLocalizationOptions> customLocalizerOptions = Options.Create(new CustomLocalizationOptions(appDirectory));

            services.AddSingleton(typeof(IStringLocalizer), x =>
            {
                return new JsonStringLocalizer(customLocalizerOptions);
            });

            return services;
        }
    }
}
