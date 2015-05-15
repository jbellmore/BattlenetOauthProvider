using Microsoft.AspNet.Authentication.OAuth;
using System;
using System.Threading.Tasks;

namespace BattlenetOauthProvider.Notifications
{
    public interface IBattlenetAuthenticationNotifications : IOAuthAuthenticationNotifications
    {
        Task Authenticated(BattlenetAuthenticatedContext context);
    }
}