using BattlenetOauthProvider.Notifications;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Security.OAuth;
using System;

namespace BattlenetOauthProvider
{
    public class BattlenetAuthenticationOptions : OAuthAuthenticationOptions<IBattlenetAuthenticationNotifications>
    {
        public BattlenetAuthenticationOptions()
        {
            AuthenticationType = BattlenetAuthorizationDefaults.AuthenticationType;
            Caption = AuthenticationType;
            CallbackPath = new PathString("/signin-battlenet");
            AuthorizationEndpoint = BattlenetAuthorizationDefaults.AuthorizationEndpoint;
            TokenEndpoint = BattlenetAuthorizationDefaults.TokenEndpoint;
        }
    }
}