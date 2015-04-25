using Microsoft.AspNet.Security.OAuth;
using System;
using System.Threading.Tasks;

namespace BattlenetOauthProvider.Notifications
{
    public interface IBattlenetAuthenticationNotifications : IOAuthAuthenticationNotifications
    {
        Task Authenticated(BattlenetAuthenticatedContext context);
    }
}