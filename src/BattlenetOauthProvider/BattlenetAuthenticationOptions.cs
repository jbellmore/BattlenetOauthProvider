using BattlenetOauthProvider.Notifications;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Authentication.OAuth;
using System;

namespace BattlenetOauthProvider
{
    public class BattlenetAuthenticationOptions : OAuthAuthenticationOptions<IBattlenetAuthenticationNotifications>
    {
        public BattlenetAuthenticationOptions()
        {
            AuthenticationScheme = BattlenetAuthorizationDefaults.AuthenticationScheme;
            Caption = AuthenticationScheme;
            CallbackPath = new PathString("/signin-battlenet");
            AuthorizationEndpoint = BattlenetAuthorizationDefaults.AuthorizationEndpoint;
            TokenEndpoint = BattlenetAuthorizationDefaults.TokenEndpoint;
            UserInformationEndpoint = BattlenetAuthorizationDefaults.UserInformationEndpoint;
        }
    }
}