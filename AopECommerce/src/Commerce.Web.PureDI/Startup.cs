using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ploeh.Samples.Commerce.Web.Presentation;

namespace Ploeh.Samples.Commerce.Web.PureDI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var appSettings = configuration.GetSection("AppSettings");

            this.Configuration = new CommerceConfiguration(
                connectionString: configuration.GetConnectionString("CommerceConnectionString"));
        }

        public CommerceConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddHttpContextAccessor();
            services.AddMvc();

            services.AddSingleton<IControllerActivator>(
                _ => new CommerceControllerActivator(this.Configuration));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
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

            app.UseStaticFiles();

            // ---- Start code snippet section 7.3.2, page 234 ----
            var logger = loggerFactory.CreateLogger("Middleware");

            app.Use(async (context, next) =>
            {
                var middleware = new LoggingMiddleware(logger);

                await middleware.Invoke(context, next);
            });
            // ---- End code snippet section 7.3.2, page 234 ----

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}