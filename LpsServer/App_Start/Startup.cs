namespace LpsServer
{
    using System;
    using System.Web.Http;

    using LpsServer.Providers;

    using Microsoft.AspNet.SignalR;
    using Microsoft.Owin;
    using Microsoft.Owin.Cors;
    using Microsoft.Owin.Security.OAuth;

    using Owin;
    using Microsoft.Owin.Security.Facebook;
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security.Google;

    public class Startup
    {
        public static FacebookAuthenticationOptions facebookAuthOptions { get; private set; }
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public static GoogleOAuth2AuthenticationOptions googleAuthOptions { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            this.ConfigureOAuth(app);


            app.UseCors(CorsOptions.AllowAll);
            app.Use<CustomAuthenticationMiddleware>();
            WebApiConfig.Register(config);
            app.UseWebApi(config);

            var hubConfiguration = new HubConfiguration();
            hubConfiguration.EnableDetailedErrors = true;
            app.MapSignalR(hubConfiguration);


            //GlobalHost.DependencyResolver.Register(typeof(JsonSerializer), () => jsonNetSerializer);
            GlobalHost.HubPipeline.RequireAuthentication();

            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            config.Filters.Add(new Filters.ExceptionFilter());
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Token Generation
            var OAuthServerOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(360),
                Provider = new SimpleAuthorizationServerProvider(),
                RefreshTokenProvider = new SimpleRefreshTokenProvider()
            };
            app.UseOAuthAuthorizationServer(OAuthServerOptions);

            OAuthBearerOptions = new OAuthBearerAuthenticationOptions()
            {
                Provider = new QueryStringOAuthBearerProvider(),

            };
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);

            //Configure Facebook External Login
            facebookAuthOptions = new FacebookAuthenticationOptions()
            {
                AppId = "243608832436774",
                AppSecret = "59ef1d54e28403d3d42b8484bc26a661",
                Provider = new FacebookAuthProvider()
            };
            app.UseFacebookAuthentication(facebookAuthOptions);

            //Configure Google External Login
            googleAuthOptions = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "660304422912-vpsqvhv83fc5eoqrd6ecka97kc8mvhtl.apps.googleusercontent.com",
                ClientSecret = "rUK43vGzVLRLRfbPuPf1Do9W",
                Provider = new GoogleAuthProvider()
            };
            app.UseGoogleAuthentication(googleAuthOptions);
        }
    }
}