using Microsoft.AspNet.Http;
using Microsoft.AspNet.Security.OAuth;
using Newtonsoft.Json.Linq;
using System;

namespace BattlenetOauthProvider.Notifications
{
    public class BattlenetAuthenticatedContext : OAuthAuthenticatedContext
    {
        protected const string AccountIdPropertyName = "accountId";
        public BattlenetAuthenticatedContext(HttpContext context, OAuthAuthenticationOptions options, JObject user, TokenResponse tokens)
            : base(context, options, user, tokens)
        {
            AccountId = TryGetAccountId(tokens.Response);
        }

        public string AccountId { get; set; }

        public string TryGetAccountId(JObject response)
        {
            JToken value;

            if(response.TryGetValue(AccountIdPropertyName, out value))
            {
                return value.ToString();
            }

            return null;
        }
    }
}