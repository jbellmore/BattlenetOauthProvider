using BattlenetOauthProvider.Notifications;
using Microsoft.AspNet.Security.OAuth;
using Microsoft.Framework.Logging;
using System;
using System.Net.Http;
using Microsoft.AspNet.Http.Security;
using Microsoft.AspNet.Security;
using System.Threading.Tasks;
using System.Security.Claims;

namespace BattlenetOauthProvider
{
    public class BattlenetAuthenticationHandler : OAuthAuthenticationHandler<BattlenetAuthenticationOptions, IBattlenetAuthenticationNotifications>
    {
        public BattlenetAuthenticationHandler(HttpClient httpClient, ILogger logger)
            : base(httpClient, logger)
        {

        }

        protected override Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            return base.AuthenticateCoreAsync();
        }

        protected override Task ApplyResponseChallengeAsync()
        {
            return base.ApplyResponseChallengeAsync();
        }

        protected override Task ApplyResponseCoreAsync()
        {
            return base.ApplyResponseCoreAsync();
        }

        protected override Task ApplyResponseGrantAsync()
        {
            return base.ApplyResponseGrantAsync();
        }

        protected override Task<TokenResponse> ExchangeCodeAsync(string code, string redirectUri)
        {
            return base.ExchangeCodeAsync(code, redirectUri);
        }        

        protected async override Task<AuthenticationTicket> GetUserInformationAsync(AuthenticationProperties properties, TokenResponse tokens)
        {
            var context = new BattlenetAuthenticatedContext(Context, Options, null, tokens);
            context.Identity = new ClaimsIdentity(
                Options.AuthenticationType,
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            if (!string.IsNullOrEmpty(context.AccountId))
            {
                context.Identity.AddClaim(new Claim("urn:battlenet:accountId", context.AccountId, ClaimValueTypes.String, Options.AuthenticationType));
            }

            context.Properties = properties;

            await Options.Notifications.Authenticated(context);

            return new AuthenticationTicket(context.Identity, context.Properties);
        }
    }
}