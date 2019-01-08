using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //客户端模式
            // https://neelbhatt.com/2018/03/04/step-by-step-setup-for-the-auth-server-and-the-client-identityserver4-with-net-core-part-ii/  身份认证
            //services.AddIdentityServer()
            //.AddDeveloperSigningCredential()   //签署标签
            //.AddInMemoryApiResources(Config.GetApiResources())   //此认证授权服务所管辖的API资源，比如上面配置的 api1，这个会在客户端调用的时候用到，如果不一致，是不允许访问的
            //.AddInMemoryClients(Config.GetClients());  //Clinet 中配置的AllowedScopes = { "api1" }，表示此种授权模式允许的API资源集合

            //密码模式
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(new List<ApiResource>
                {
                    new ApiResource("api_name1","My API")
                })
                .AddInMemoryClients(new List<Client>
                {
                    new Client
                    {
                        ClientId = "Client_id_1",
                        AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,  //密码模式
                        ClientSecrets =
                        {
                            new Secret("secret".Sha256())
                        },
                        AllowedScopes = { "api_name1" }
                    }
                }).AddTestUsers(new List<TestUser>
                {
                    new TestUser
                    {
                        SubjectId = "1",
                        Username = "xishuai",
                        Password = "123"
                    }
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //增加身份认证
            app.UseIdentityServer();


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
