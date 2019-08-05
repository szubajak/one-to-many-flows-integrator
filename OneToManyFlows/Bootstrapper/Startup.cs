namespace OneToManyFlows.Bootstrapper
{
    using System;
    using System.Net;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using Core;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    public class Startup : IStartup
    {
        private readonly IContainerSetup _containerSetup;

        private readonly IHostingEnvironment _environment;

        public Startup(IConfiguration configuration, IContainerSetup containerSetup, IHostingEnvironment environment)
        {
            _containerSetup = containerSetup;
            _environment = environment;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            services.AddHttpClient();

            services.AddHttpContextAccessor();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "OneToManyFlows", Version = "v1" });
            });

            var builder = new ContainerBuilder();

            builder.Populate(services);

            _containerSetup.RegisterServices(builder);

            return new AutofacServiceProvider(builder.Build());
        }

        public void Configure(IApplicationBuilder app)
        {
            if (!_environment.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "text/plain";

                    // Do something with exception
                    var exceptionHandler = context.Features.Get<IExceptionHandlerPathFeature>();

                    await context.Response.WriteAsync(Errors.UnexpectedError);
                });
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "OneToManyFlows API V1");
                options.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }
    }
}