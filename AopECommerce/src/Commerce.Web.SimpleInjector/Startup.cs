using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ploeh.Samples.Commerce.Domain;
using Ploeh.Samples.Commerce.Domain.EventHandlers;
using Ploeh.Samples.Commerce.ExternalConnections;
using Ploeh.Samples.Commerce.SqlDataAccess;
using Ploeh.Samples.Commerce.SqlDataAccess.Aspects;
using Ploeh.Samples.Commerce.Web.Presentation;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;

namespace Commerce.Web.SimpleInjector
{
    public class Startup
    {
        private readonly Container container = new Container();

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
            // ASP.NET default stuff here
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            IntegrateSimpleInjector(services);
        }

        private void IntegrateSimpleInjector(IServiceCollection services)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddHttpContextAccessor();
            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(container));
            services.AddSingleton<IViewComponentActivator>(new SimpleInjectorViewComponentActivator(container));

            services.EnableSimpleInjectorCrossWiring(container);
            services.UseSimpleInjectorAspNetRequestScoping(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            InitializeContainer(app);

            container.Verify();

            // ASP.NET default stuff here
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void InitializeContainer(IApplicationBuilder app)
        {
            // Add application presentation components:
            container.RegisterMvcControllers(app);

            container.RegisterSingleton<ITimeProvider, DefaultTimeProvider>();
            container.RegisterInstance<IUserContext>(new AspNetUserContextAdapter());
            container.Register(() => new CommerceContext(this.Configuration.ConnectionString),
                Lifestyle.Scoped);

            Assembly assembly = typeof(ITimeProvider).Assembly;

            // ---- Start code Listing 14.11 ----
            container.Register(
                typeof(ICommandService<>), assembly);

            container.RegisterDecorator(
                typeof(ICommandService<>),
                typeof(AuditingCommandServiceDecorator<>));

            // NOTE: Ambient transactions disabled as SQLite does not support it. You can turn it on 
            // after switching to SQL Server by uncommenting the next line.
            // container.RegisterDecorator(
            //     typeof(ICommandService<>),
            //     typeof(TransactionCommandServiceDecorator<>));

            container.RegisterDecorator(
                typeof(ICommandService<>),
                typeof(SaveChangesCommandServiceDecorator<>));

            container.RegisterDecorator(
                typeof(ICommandService<>),
                typeof(SecureCommandServiceDecorator<>));
            // ---- End code Listing 14.11 ----

            // ---- Start code Section 14.4.5 ----
            container.Collection.Register(typeof(IEventHandler<>), assembly);
            container.Register(typeof(IEventHandler<>), typeof(CompositeEventHandler<>));
            // ---- End code Section 14.4.5 ----

            // Register adapters to external systems
            this.RegisterAsImplementedInterfaces(typeof(WcfBillingSystem).Assembly, type => true);

            // Register repositories
            this.RegisterAsImplementedInterfaces(typeof(SqlProductRepository).Assembly,
                type => type.Name.EndsWith("Repository"));
        }

        private void RegisterAsImplementedInterfaces(Assembly asm, Func<Type, bool> predicate) =>
            this.RegisterAsImplementedInterfaces(asm.ExportedTypes.Where(predicate));

        private void RegisterAsImplementedInterfaces(IEnumerable<Type> implementationTypes)
        {
            foreach (Type type in implementationTypes)
            {
                foreach (Type service in type.GetInterfaces())
                {
                    this.container.Register(service, type);
                }
            }
        }
    }
}