﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using IdentityModel;

namespace MVCClient
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //This method gets called by the runtime.Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddMvc();
        //    JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        //    services.AddAuthentication(options =>
        //    {
        //        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        //        options.DefaultChallengeScheme = "oidc";
        //    })
        //    .AddCookie("Cookies")
        //    .AddOpenIdConnect("oidc", options =>
        //    {
        //        options.SignInScheme = "Cookies";
        //        options.Authority = "http://localhost:5000";
        //        options.RequireHttpsMetadata = false;

        //        options.ClientId = "mvc";
        //        options.ClientSecret = "secret";
        //        options.ResponseType = "code id_token";

        //        options.SaveTokens = true;
        //        options.GetClaimsFromUserInfoEndpoint = true;
        //        //options.CallbackPath = "/Home/About";

        //        options.Scope.Add("api1");
        //        options.Scope.Add("offline_access");
        //    });
        //}

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies")
                //.AddCookie(options =>
                //{
                //    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                //    options.Cookie.Name = "mvchybrid";
                //})
                .AddOpenIdConnect("oidc", options =>
                {
                    options.SignInScheme = "Cookies";
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;

                    //options.ClientId = "mvc";
                    options.ClientId = "mvc";
                    options.ClientSecret = "secret";
                    options.ResponseType = "code id_token";

                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;

                    //options.Scope.Clear();
                    //options.Scope.Add("openid");
                    //options.Scope.Add("profile");
                    //options.Scope.Add("email");
                    options.Scope.Add("api1");
                    options.Scope.Add("offline_access");

                    //options.ClaimActions.Remove("amr");
                    //options.ClaimActions.MapJsonKey("website", "website");




                    //options.TokenValidationParameters = new TokenValidationParameters
                    //{
                    //    NameClaimType = JwtClaimTypes.Name,
                    //    RoleClaimType = JwtClaimTypes.Role,
                    //};
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            app.UseAuthentication();
            

            app.UseStaticFiles();

            //app.UseMvcWithDefaultRoute();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
