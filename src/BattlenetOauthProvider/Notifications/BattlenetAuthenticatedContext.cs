using Microsoft.AspNet.Authentication.OAuth;
using Microsoft.AspNet.Http;
using Newtonsoft.Json.Linq;
using System;

namespace BattlenetOauthProvider.Notifications
{
    public class BattlenetAuthenticatedContext : OAuthAuthenticatedContext
    {
        protected const string AccountIdPropertyName = "accountId";
        protected const string BattletagPropertyName = "battletag";
        public BattlenetAuthenticatedContext(HttpContext context, OAuthAuthenticationOptions options, JObject user, TokenResponse tokens)
            : base(context, options, user, tokens)
        {
            AccountId = TryGetAccountId(tokens.Response);
            BattleTag = TryGetBattletag(user);
        }

        public string AccountId { get; set; }
        public string BattleTag { get; set; }

        public string TryGetBattletag(JObject user)
        {
            JToken value;

            if(user != null)
            {
                if (user.TryGetValue(BattletagPropertyName, out value))
                {
                    return value.ToString();
                }
            }

            return null;
        }

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