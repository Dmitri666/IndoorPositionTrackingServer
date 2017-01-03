// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryStringOAuthBearerProvider.cs" company="">
//   
// </copyright>
// <summary>
//   The query string o auth bearer provider.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LpsServer
{
    using System.Threading.Tasks;

    using Microsoft.Owin.Security.OAuth;

    /// <summary>
    /// The query string o auth bearer provider.
    /// </summary>
    public class QueryStringOAuthBearerProvider : OAuthBearerAuthenticationProvider
    {
        #region Public Methods and Operators

        /// <summary>
        /// The request token.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override Task RequestToken(OAuthRequestTokenContext context)
        {
            var value = context.Request.Query.Get("access_token");

            if (!string.IsNullOrEmpty(value))
            {
                context.Token = value;
                return Task.FromResult<object>(null);
            }

            return base.RequestToken(context);
            
            
        }

        #endregion
    }
}