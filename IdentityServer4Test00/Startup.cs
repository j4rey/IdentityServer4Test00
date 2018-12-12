using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer4.Quickstart.UI;
using IdentityServer4;

namespace IdentityServer4Test00
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddIdentityServer()
            .AddDeveloperSigningCredential()
            .AddInMemoryIdentityResources(Config.GetIdentityResources())
            .AddInMemoryApiResources(Config.GetApiResources())
            .AddInMemoryClients(Config.GetClients())
            //.AddTestUsers(Config.GetUsers());
            .AddTestUsers(TestUsers.Users);

            services.AddAuthentication()
                .AddFacebook("Facebook", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ClientId = "###client_id###";
                    options.ClientSecret = "###client_secret###";
                });
            //Additional External Provider
            //.AddOpenIdConnect("oidc", "OpenID Connect", options =>
            // {
            //     options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
            //     options.SignOutScheme = IdentityServerConstants.SignoutScheme;

            //     options.Authority = "https://demo.identityserver.io/";
            //     options.ClientId = "implicit";

            //     options.TokenValidationParameters = new TokenValidationParameters
            //     {
            //         NameClaimType = "name",
            //         RoleClaimType = "role"
            //     };
            // });

            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("default");
            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvcWithDefaultRoute();
            //app.UseCors("AngularPolicy");
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
