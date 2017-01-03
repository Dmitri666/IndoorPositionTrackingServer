// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleAuthorizationServerProvider.cs" company="">
//   
// </copyright>
// <summary>
//   The simple authorization server provider.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LpsServer
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Lps.Services;

    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;

    /// <summary>
    /// The simple authorization server provider.
    /// </summary>
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        #region Public Methods and Operators

        /// <summary>
        /// The grant refresh token.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            // Change auth ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);

            var newClaim = newIdentity.Claims.Where(c => c.Type == "newClaim").FirstOrDefault();
            if (newClaim != null)
            {
                newIdentity.RemoveClaim(newClaim);
            }

            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// The grant resource owner credentials.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin") ?? "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            var repo = new LpsRepository();
            var user = repo.GetUserData(context.UserName);
            if (user == null)
            {
                context.Response.Headers.Add(
                    "X-Challenge",
                    new[] { ((int)HttpStatusCode.UnsupportedMediaType).ToString() });
                context.SetError("unknown_user", "The user name is incorrect.");
                return;
            }

            if (user.Password != context.Password)
            {
                context.Response.Headers.Add(
                    "X-Challenge", 
                    new[] { ((int)HttpStatusCode.UnsupportedMediaType).ToString() });
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            foreach (var role in user.Roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
                
            }

            if (!string.IsNullOrEmpty(context.ClientId))
            {
                repo.RegisterUserCurrentDevice(context.UserName, context.ClientId);
            }

            var props = new AuthenticationProperties(new Dictionary<string, string>
                                                    {
                                                        { "as:client_id", context.ClientId ?? string.Empty }, 
                                                        { "userName", context.UserName },
                                                        { "userRole", user.MainUserRole }
                                                    });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
        }

        /// <summary>
        /// The token endpoint.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// The validate client authentication.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            context.Validated();
        }

        #endregion
    }
}