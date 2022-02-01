using AuthorizationService;
using AuthorizationService.ProductPolicies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProductMicroservice.Controllers;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DotNetCoreAPIMicroservice_POC
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
            //var provider = services.BuildServiceProvider().GetRequiredService<IActionDescriptorCollectionProvider>();
            //var ctrlActions = provider.ActionDescriptors.Items
            //        .Where(x => (x as ControllerActionDescriptor)
            //        .ControllerTypeInfo.AsType() == typeof(ProductController))
            //        .ToList();
            //foreach (var action in ctrlActions)
            //{
            //    var descriptor = action as ControllerActionDescriptor;
            //    var controllerName = descriptor.ControllerName;
            //    var actionName = descriptor.ActionName;
            //    var areaName = descriptor.ControllerTypeInfo
            //           .GetCustomAttribute<AreaAttribute>().RouteValue;
            //}

            //var appSettingSection = Configuration.GetSection("AppSettings");
            //services.Configure<AppSettings>(appSettingSection);

            //var appSettings = appSettingSection.Get<AppSettings>();
            //var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            services.AddAuthentication(AzureADB2CDefaults.BearerAuthenticationScheme)
                .AddAzureADB2CBearer(options => Configuration.Bind("AzureAdB2C", options));

            


            //SetupJWTServices(services);

            // Add custom authorization handlers
            services.AddAuthorization(options =>
            {
                //options.AddPolicy("DesignationSrLead", policy => policy.Requirements.Add(new DesignationRequirement(DesignationEnum.SrLead)));
                options.AddPolicy("ProductSharedPolicy", policy => policy.Requirements.Add(new ProductSharedPolicy()));
                //options.AddPolicy("AgeAbove27Consultant", policy => policy.Requirements.Add(new MaximumAgeRoleDesignationRequirement(27, DesignationEnum.Consultant)));
            });

            



            //services.AddSingleton<IAuthorizationHandler, DesignationAuthorizationHandler>();
            //services.AddSingleton<IAuthorizationHandler, DesignationAgeAuthorizationHandler>();
            
            services.AddSingleton<IAuthorizationHandler, ProductSharedPolicyHandler>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();   //similar to => services.AddHttpContextAccessor();
            services.AddScoped<Repository.IProductRepository, Repository.ProductRepository>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        private void SetupJWTServices(IServiceCollection services)
        {
            string key = "MySuperSecuredKey"; //this should be same which is used while creating token      
            var issuer = "http://mysite.com";  //this should be same which is used while creating token  

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = issuer,
                    ValidIssuer = issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });
            services.AddCors(o =>
            {
                o.AddPolicy("CorsPolicy", builder =>
                {
                    builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .SetIsOriginAllowed(_ => true);
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

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseCors("CorsPolicy");

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
