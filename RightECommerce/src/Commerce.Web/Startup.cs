using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Ploeh.Samples.Commerce.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddHttpContextAccessor();

            // Add custom Pure DI Controller Activator
            var appSettings = this.Configuration.GetSection("AppSettings");

            var controllerActivator = new CommerceControllerActivator(
                configuration: new CommerceConfiguration(
                    connectionString: this.Configuration.GetConnectionString("CommerceConnectionString"),
                    productRepositoryTypeName: appSettings.GetValue<string>("ProductRepositoryType")));

            services.AddSingleton<IControllerActivator>(controllerActivator);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            ConfigureMiddleware(app, loggerFactory);

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static void ConfigureMiddleware(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            // ---- Start code Section 7.3.2 ----
            var logger = loggerFactory.CreateLogger("Middleware");

            app.Use(async (context, next) =>
            {
                var middleware = new LoggingMiddleware(logger);

                await middleware.Invoke(context, next);
            });
            // ---- End code Section 7.3.2 ----
        }
    }
}