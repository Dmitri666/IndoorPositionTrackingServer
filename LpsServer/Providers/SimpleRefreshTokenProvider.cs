// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleRefreshTokenProvider.cs" company="">
//   
// </copyright>
// <summary>
//   The simple refresh token provider.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LpsServer.Providers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Owin.Security.Infrastructure;

    /// <summary>
    /// The simple refresh token provider.
    /// </summary>
    public class SimpleRefreshTokenProvider : IAuthenticationTokenProvider
    {
        #region Public Methods and Operators

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The create async.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            context.Ticket.Properties.IssuedUtc = DateTime.UtcNow;
            context.Ticket.Properties.ExpiresUtc = DateTime.UtcNow.AddYears(10);
            context.SetToken(context.SerializeTicket());
        }

        /// <summary>
        /// The receive.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The receive async.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin") ?? "*";
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
            context.DeserializeTicket(context.Token);
        }

        #endregion
    }
}