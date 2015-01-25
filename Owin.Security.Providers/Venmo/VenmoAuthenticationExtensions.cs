using System;

namespace Owin.Security.Providers.Venmo
{
    public static class VenmoAuthenticationExtensions
    {
        public static IAppBuilder UseVenmoAuthentication(this IAppBuilder app,
            VenmoAuthenticationOptions options)
        {
            if (app == null)
                throw new ArgumentNullException("app");
            if (options == null)
                throw new ArgumentNullException("options");

            app.Use(typeof(VenmoAuthenticationMiddleware), app, options);

            return app;
        }

        public static IAppBuilder UseVenmoAuthentication(this IAppBuilder app, string clientId, string clientSecret)
        {
            return app.UseVenmoAuthentication(new VenmoAuthenticationOptions
            {
                ClientId = clientId,
                ClientSecret = clientSecret
            });
        }
    }
}