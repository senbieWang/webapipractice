using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApiStandardTest.Filter;
using WebApiStandardTest.RequestModelValidation;


//https://code-maze.com/aspnetcore-webapi-best-practices/   ASP.NET Core Web API Best Practices
//https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-2.1  fundamentails

namespace WebApiStandardTest
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
            //Identity server4
            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "api1";
                });

            
            //注册MVC服务
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddFluentValidation();
            var mvc = services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //FluentValidation
            services.ConfigureFluentValidation(mvc);

            //添加日志模块   ？？  2019-1-4           


            //参数校验
            //services

            //bad request 处理 
            //services.AddScoped<ModelValidactionAttribute>();

            //配置跨域访问
            services.ConfigureCors();

            //注册swagger 文档生成服务
            services.ConfigureSwaggers();

            //注册版本管理服务
            services.ConfigureApiVersioning();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            //开发环境
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //生产环境
            if ( env.IsProduction() || env.IsStaging() || env.IsEnvironment("Staging_2") )
            {             
                app.UseHsts();
            }

            app.UseAuthentication();

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI( options =>
            {
                // build a swagger endpoint for each discovered API version
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });
        }
    }
}
