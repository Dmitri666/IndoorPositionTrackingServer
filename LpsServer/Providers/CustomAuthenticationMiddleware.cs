using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LpsServer
{
    using System.Threading.Tasks;

    using Microsoft.Owin;

    public class CustomAuthenticationMiddleware : OwinMiddleware
    {
        public CustomAuthenticationMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            await Next.Invoke(context);

            if (context.Response.StatusCode == 400
                && context.Response.Headers.ContainsKey(
                          "X-Challenge"))
            {
                var headerValues = context.Response.Headers.GetValues
                      ("X-Challenge");

                context.Response.StatusCode =
                       Convert.ToInt16(headerValues.FirstOrDefault());

                context.Response.Headers.Remove("X-Challenge");
            }

        }
    }
}