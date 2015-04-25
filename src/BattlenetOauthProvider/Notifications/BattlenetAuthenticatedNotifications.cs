using Microsoft.AspNet.Security.OAuth;
using System;
using System.Threading.Tasks;

namespace BattlenetOauthProvider.Notifications
{
    public class BattlenetAuthenticatedNotifications : OAuthAuthenticationNotifications, IBattlenetAuthenticationNotifications
    {
        public BattlenetAuthenticatedNotifications()
        {
            OnAuthenticated = context => Task.FromResult<object>(null);
        }

        public Func<BattlenetAuthenticatedContext, Task> OnAuthenticated { get; set; }

        public virtual Task Authenticated(BattlenetAuthenticatedContext context)
        {
            return OnAuthenticated(context);
        }
    }
}