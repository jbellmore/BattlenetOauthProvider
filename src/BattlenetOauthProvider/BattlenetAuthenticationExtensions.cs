using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.OptionsModel;
using System;

namespace BattlenetOauthProvider
{
    public static class BattlenetAuthenticationExtensions
    {
        public static IServiceCollection ConfigureBattlenetAuthentication([NotNull] this IServiceCollection services, [NotNull] Action<BattlenetAuthenticationOptions> configure)
        {
            return services.Configure(configure);
        }

        public static IApplicationBuilder UseBattlenetAuthentication([NotNull] this IApplicationBuilder app, Action<BattlenetAuthenticationOptions> configureOptions = null, string optionsName = "")
        {
            return app.UseMiddleware<BattlenetAuthenticationMiddleware>(
                 new ConfigureOptions<BattlenetAuthenticationOptions>(configureOptions ?? (o => { }))
                 {
                     Name = optionsName
                 });
        }
    }
}