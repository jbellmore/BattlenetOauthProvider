using BattlenetOauthProvider.Notifications;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Security;
using Microsoft.AspNet.Security.DataProtection;
using Microsoft.AspNet.Security.Infrastructure;
using Microsoft.AspNet.Security.OAuth;
using Microsoft.Framework.Logging;
using Microsoft.Framework.OptionsModel;
using System;

namespace BattlenetOauthProvider
{
    public class BattlenetAuthenticationMiddleware : OAuthAuthenticationMiddleware<BattlenetAuthenticationOptions, IBattlenetAuthenticationNotifications>
    {
        public BattlenetAuthenticationMiddleware(
            RequestDelegate next,
            IServiceProvider services,
            IDataProtectionProvider dataProtectionProvider,
            ILoggerFactory loggerFactory,
            IOptions<ExternalAuthenticationOptions> externalOptions,
            IOptions<BattlenetAuthenticationOptions> options,
            ConfigureOptions<BattlenetAuthenticationOptions> configureOptions = null)
            : base(next, services, dataProtectionProvider, loggerFactory, externalOptions, options, configureOptions)
        {
            if (Options.Notifications == null)
            {
                Options.Notifications = new BattlenetAuthenticatedNotifications();
            }

            if (Options.Scope.Count == 0)
            {
                // TODO: Set default scope
            }
        }

        protected override AuthenticationHandler<BattlenetAuthenticationOptions> CreateHandler()
        {
            return new BattlenetAuthenticationHandler(Backchannel, Logger);
        }
    }
}