using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ploeh.Samples.Commerce.Domain;
using Ploeh.Samples.Commerce.Domain.CommandServices;
using Ploeh.Samples.Commerce.ExternalConnections;
using Ploeh.Samples.Commerce.SqlDataAccess;
using Ploeh.Samples.Commerce.SqlDataAccess.Aspects;
using Ploeh.Samples.Commerce.Web.Presentation;

namespace Ploeh.Samples.Commerce.Web.MS.DI
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<ITimeProvider, DefaultTimeProvider>();
            services.AddSingleton<IUserContext>(new AspNetUserContextAdapter());
            services.AddScoped(_ => new CommerceContext(this.Configuration.ConnectionString));

            // ---- Start code Listing 15.7 ----
            Assembly assembly = typeof(AdjustInventoryService).Assembly;

            // Register ICommandService<T> implementations with their decorators
            var mappings =
                from type in assembly.GetTypes()
                where !type.IsAbstract
                where !type.IsGenericType
                from i in type.GetInterfaces()
                where i.IsGenericType
                where i.GetGenericTypeDefinition()
                    == typeof(ICommandService<>)
                select new { service = i, implementation = type };

            foreach (var mapping in mappings)
            {
                Type commandType = mapping.service.GetGenericArguments()[0];

                Type secureDecoratoryType =
                    typeof(SecureCommandServiceDecorator<>)
                        .MakeGenericType(commandType);

                Type saveChangesDecoratorType =
                    typeof(SaveChangesCommandServiceDecorator<>)
                        .MakeGenericType(commandType);

                Type auditingDecoratorType =
                    typeof(AuditingCommandServiceDecorator<>)
                        .MakeGenericType(commandType);

                // NOTE: Ambient transactions disabled as SQLite does not support it. You can turn it on 
                // after switching to SQL Server by wrapping the TransactionCommandServiceDecorator<TCommand>
                // around the SaveChangesCommandServiceDecorator<TCommand>.
                services.AddTransient(mapping.service, c =>
                    ActivatorUtilities.CreateInstance(
                        c,
                        secureDecoratoryType,
                        ActivatorUtilities.CreateInstance(
                            c,
                            saveChangesDecoratorType,
                            ActivatorUtilities.CreateInstance(
                                c,
                                auditingDecoratorType,
                                ActivatorUtilities.CreateInstance(
                                    c,
                                    mapping.implementation)))));
            }
            // ---- End code Listing 15.7 ----

            // Register IEventHandler<T> implementations
            // ---- Start code Listing 15.11 ----
            var handlerTypes =
                from type in assembly.GetTypes()
                where !type.IsAbstract
                where !type.IsGenericType
                let serviceTypes = type.GetInterfaces()
                    .Where(i => i.IsGenericType &&
                        i.GetGenericTypeDefinition()
                            == typeof(IEventHandler<>))
                where serviceTypes.Any()
                select type;

            services.AddSingleton(new CompositeSettings
            {
                AllHandlerTypes = handlerTypes.ToArray()
            });

            services.AddTransient(
                typeof(IEventHandler<>),
                typeof(MsDiCompositeEventHandler<>));
            // ---- End code Listing 15.11 ----

            // Register adapters to external systems
            this.RegisterAsImplementedInterfaces(services, typeof(WcfBillingSystem).Assembly, type => true);

            // Register repositories
            this.RegisterAsImplementedInterfaces(services, typeof(SqlProductRepository).Assembly,
                type => type.Name.EndsWith("Repository"));
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

        private void RegisterAsImplementedInterfaces(
            IServiceCollection services, Assembly asm, Func<Type, bool> predicate)
        {
            this.RegisterAsImplementedInterfaces(services, asm.ExportedTypes.Where(predicate));
        }

        private void RegisterAsImplementedInterfaces(
            IServiceCollection services, IEnumerable<Type> implementationTypes)
        {
            foreach (Type type in implementationTypes)
                foreach (Type service in type.GetInterfaces())
                    services.AddTransient(service, type);
        }

        // ---- Start code Listing 15.10 ----
        public class CompositeSettings
        {
            public Type[] AllHandlerTypes { get; set; }
        }

        public class MsDiCompositeEventHandler<TEvent> : IEventHandler<TEvent>
        {
            private readonly IServiceProvider provider;
            private readonly CompositeSettings settings;

            public MsDiCompositeEventHandler(
                IServiceProvider provider,
                CompositeSettings settings)
            {
                if (provider == null) throw new ArgumentNullException(nameof(provider));
                if (settings == null) throw new ArgumentNullException(nameof(settings));

                this.provider = provider;
                this.settings = settings;
            }

            public void Handle(TEvent e)
            {
                if (e == null) throw new ArgumentNullException(nameof(e));

                foreach (var handler in this.GetHandlers())
                {
                    handler.Handle(e);
                }
            }

            private IEnumerable<IEventHandler<TEvent>> GetHandlers()
            {
                return
                    from type in this.settings.AllHandlerTypes
                    where typeof(IEventHandler<TEvent>)
                        .IsAssignableFrom(type)
                    select (IEventHandler<TEvent>)
                        ActivatorUtilities.CreateInstance(
                            this.provider, type);
            }
        }
        // ---- End code Listing 15.10 ----
    }
}