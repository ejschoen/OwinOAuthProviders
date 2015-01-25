// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Security.Claims;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;
using Newtonsoft.Json.Linq;

namespace Owin.Security.Providers.Venmo
{
    /// <summary>
    /// Contains information about the login session as well as the user <see cref="System.Security.Claims.ClaimsIdentity"/>.
    /// </summary>
    public class VenmoAuthenticatedContext : BaseContext
    {
        /// <summary>
        /// Initializes a <see cref="VenmoAuthenticatedContext"/>
        /// </summary>
        /// <param name="context">The OWIN environment</param>
        /// <param name="user">The JSON-serialized user</param>
        /// <param name="accessToken">Venmo Access token</param>
        /// <param name="refreshToken">Venmo Refresh token</param>
        public VenmoAuthenticatedContext(IOwinContext context, JObject user, string accessToken, string refreshToken)
            : base(context)
        {
            User = user;
            AccessToken = accessToken;
            RefreshToken = refreshToken;

            UserName = TryGetValue(user, "username");
            FirstName = TryGetValue(user, "first_name");
            LastName = TryGetValue(user, "last_name");
            DisplayName = TryGetValue(user, "display_name");
            Email = TryGetValue(user, "email");
            Phone = TryGetValue(user, "phone");
            Id = TryGetValue(user, "id");
        }

        /// <summary>
        /// Gets the JSON-serialized user
        /// </summary>
        /// <remarks>
        /// Contains the Venmo user obtained from the User Info endpoint. By default this is https://api.Venmo.com/user but it can be
        /// overridden in the options
        /// </remarks>
        public JObject User { get; private set; }

        /// <summary>
        /// Gets the Venmo access token
        /// </summary>
        public string AccessToken { get; private set; }

        /// <summary>
        /// Gets the Venmo refresh token, if the application's scope allows it
        /// </summary>
        public string RefreshToken { get; private set; }

        /// <summary>
        /// Gets the Venmo ID / User Info Endpoint
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets the Venmo User Name
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// Gets the user's name
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// Gets the Venmo User Email
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Gets the Venmo user phone number.
        /// </summary>
        public string Phone { get; private set; }

        /// <summary>
        /// Gets the user's First Name
        /// </summary>
        public string FirstName { get; private set; }

        /// <summary>
        /// Gets the user's name
        /// </summary>
        public string LastName { get; private set; }

        /// <summary>
        /// Gets the <see cref="ClaimsIdentity"/> representing the user
        /// </summary>
        public ClaimsIdentity Identity { get; set; }

        /// <summary>
        /// Gets or sets a property bag for common authentication properties
        /// </summary>
        public AuthenticationProperties Properties { get; set; }

        private static string TryGetValue(JObject user, string propertyName)
        {
            JToken value;
            return user.TryGetValue(propertyName, out value) ? value.ToString() : null;
        }
    }
}
