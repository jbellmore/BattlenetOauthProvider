using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Http.Authentication;
using Microsoft.AspNet.Authentication.OAuth;
using Microsoft.AspNet.WebUtilities;
using BattlenetOauthProvider.Notifications;
using Microsoft.Framework.Logging;
using Microsoft.AspNet.Authentication;
using Newtonsoft.Json.Linq;

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
            // Get the Google user
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Options.UserInformationEndpoint);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);
            HttpResponseMessage backChannelResponse = await Backchannel.SendAsync(request, Context.RequestAborted);
            backChannelResponse.EnsureSuccessStatusCode();
            var text = await backChannelResponse.Content.ReadAsStringAsync();
            JObject user = JObject.Parse(text);

            var context = new BattlenetAuthenticatedContext(Context, Options, user, tokens);
            var identity = new ClaimsIdentity(
                Options.AuthenticationScheme,
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            if (!string.IsNullOrEmpty(context.AccountId))
            {
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, context.AccountId, ClaimValueTypes.String, Options.AuthenticationScheme));
                if (!string.IsNullOrEmpty(context.BattleTag))
                {
                    identity.AddClaim(new Claim("urn:battlenet:battletag", context.BattleTag));
                }
            }

            identity.AddClaim(new Claim("urn:battlenet:access_token", tokens.AccessToken));

            context.Properties = properties;
            context.Principal = new ClaimsPrincipal(identity);

            await Options.Notifications.Authenticated(context);

            return new AuthenticationTicket(context.Principal, context.Properties, context.Options.AuthenticationScheme);
        }
    }
}