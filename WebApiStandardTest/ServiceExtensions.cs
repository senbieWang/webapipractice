using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using WebApiStandardTest.Filter;
using WebApiStandardTest.RequestModelValidation;

namespace WebApiStandardTest
{
    public static class ServiceExtensions
    {
        //添加跨域访问
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options => 
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()   //任何来源的主机访问
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        // Register the Swagger generator, defining 1 or more Swagger documents
        //public static void ConfigureSwaggers(this IServiceCollection services)
        //{
        //    services.AddSwaggerGen(c =>
        //    {
        //        c.SwaggerDoc("BimsQuery", new Info { Title = "Bims DataQuery API", Version = "v1" });
        //        c.SwaggerDoc("BimsManager", new Info { Title = "BIMs DataManager API", Version = "v1" });

        //        c.DocInclusionPredicate((docName, apiDesc) => {
        //            if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;
        //            var versions = methodInfo.DeclaringType
        //                                    .GetCustomAttributes(true)
        //                                    .OfType<ApiExplorerSettingsAttribute>()
        //                                    .Select(attr => attr.GroupName);
        //            if (docName.ToLower() == "BimsManager" && versions.FirstOrDefault() == null)
        //            {
        //                return true;  //无ApiExplorerSettingsAttribute 属性的在other中显示
        //            }
        //            return versions.Any(v => v == docName);
        //        });

        //        c.DocumentFilter<EnumDocumentFilter>();
        //        //c.OperationFilter<SwaggerSupportFileUploadOperationFilter>();

        //        // Set the comments path for the Swagger JSON and UI.
        //        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        //        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        //        c.IncludeXmlComments(xmlPath);
        //    });
        //}

        //web api 版本控制
        //public static void ConfigureApiVersioning(this IServiceCollection services)
        //{
        //    services.AddApiVersioning(options =>
        //    {
        //        //options.ApiVersionReader = new QueryStringApiVersionReader();
        //        //options.AssumeDefaultVersionWhenUnspecified = true;
        //        //options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
        //    });
        //}


        public static void ConfigureSwaggers(this IServiceCollection services)
        {
            services.AddSwaggerGen(
                options => 
                {
                    // resolve the IApiVersionDescriptionProvider service
                    // note: that we have to build a temporary service provider here because one has not been created yet
                    var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                    // add a swagger document for each discovered API version
                    // note: you might choose to skip or document deprecated API versions differently
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
                    }
                    // add a custom operation filter which sets default values
                    options.OperationFilter<SwaggerDefaultValues>();
                    // integrate xml comments
                    options.IncludeXmlComments(XmlCommentsFilePath);
                });
        }

        //web api 版本控制
        public static void ConfigureApiVersioning(this IServiceCollection services)
        {
            services.AddMvcCore().AddVersionedApiExplorer(
                options => 
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            services.AddApiVersioning(o => o.ReportApiVersions = true);
        }

        static string XmlCommentsFilePath
        {
            get
            {
                var basePath = AppContext.BaseDirectory;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }
        static Info CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new Info()
            {
                Title = $"BimsDataCenter API {description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
                Description = "BimsDataCenter application with Swagger, Swashbuckle, and API versioning."
            };
            if (description.IsDeprecated)
            {
                info.Description += "This API version has been deprecated.";
            }
            return info;
        }

        //
        public static void ConfigureFluentValidation(this IServiceCollection services, IMvcBuilder mvc)
        {            
            mvc.AddFluentValidation();
            services.AddTransient<IValidator<V2.Models.Subdata>, SubdataValidator>();
        }

    }
}
