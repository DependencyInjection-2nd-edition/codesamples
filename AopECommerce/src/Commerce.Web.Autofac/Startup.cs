using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ploeh.Samples.Commerce.Domain;
using Ploeh.Samples.Commerce.Domain.EventHandlers;
using Ploeh.Samples.Commerce.ExternalConnections;
using Ploeh.Samples.Commerce.SqlDataAccess;
using Ploeh.Samples.Commerce.SqlDataAccess.Aspects;
using Ploeh.Samples.Commerce.Web.Presentation;

namespace Commerce.Web.Autofac
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc()
                .AddControllersAsServices()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you. If you
        // need a reference to the container, you need to use the
        // "Without ConfigureContainer" mechanism shown later.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            Assembly domainAssembly = typeof(ITimeProvider).Assembly;

            builder.RegisterType<DefaultTimeProvider>().As<ITimeProvider>().SingleInstance();
            builder.RegisterInstance<IUserContext>(new AspNetUserContextAdapter());
            builder.Register(c => new CommerceContext(this.Configuration.ConnectionString))
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(domainAssembly).AsClosedTypesOf(typeof(ICommandService<>));

            builder.RegisterGenericDecorator(
                typeof(AuditingCommandServiceDecorator<>), typeof(ICommandService<>));

            // NOTE: Ambient transactions disabled as SQLite does not support it. You can turn it on 
            // after switching to SQL Server by uncommenting the next line.
            //builder.RegisterGenericDecorator(typeof(TransactionCommandServiceDecorator<>), typeof(ICommandService<>));

            builder.RegisterGenericDecorator(
                typeof(SaveChangesCommandServiceDecorator<>), typeof(ICommandService<>));
            builder.RegisterGenericDecorator(
                typeof(SecureCommandServiceDecorator<>), typeof(ICommandService<>));

            // ---- Start code section 13.4.4 ----
            builder.RegisterAssemblyTypes(domainAssembly)
                .As(type =>
                    from interfaceType in type.GetInterfaces()
                    where interfaceType.IsClosedTypeOf(typeof(IEventHandler<>))
                    select new KeyedService("handler", interfaceType));

            builder.RegisterGeneric(typeof(CompositeEventHandler<>))
                .As(typeof(IEventHandler<>))
                .WithParameter(
                    (p, c) => true,
                    (p, c) => c.ResolveNamed("handler", p.ParameterType));
            // ---- End code section 13.4.4 ----

            // Register adapters to external systems
            builder.RegisterAssemblyTypes(typeof(WcfBillingSystem).Assembly)
                .AsImplementedInterfaces();

            // Register repositories
            builder.RegisterAssemblyTypes(typeof(SqlProductRepository).Assembly)
                .Where(type => type.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();
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
    }
}