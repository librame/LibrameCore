using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Librame.AspNetCore.Api.Examples
{
    using Extensions.Data;
    using Identity;
    using Identity.Api;

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
            //var defaultConnectionString = "Data Source=.;Initial Catalog=librame_identity_default;Integrated Security=True";
            var writeConnectionString = "Data Source=.;Initial Catalog=librame_identity_write;Integrated Security=True";

            // Add Librame for ASP.NET Core
            services.AddLibrameCore()
                .AddDataCore(options =>
                {
                    // Ĭ��ʹ��д�����Ϊ����
                    options.DefaultTenant.DefaultConnectionString = writeConnectionString;
                    options.DefaultTenant.WriteConnectionString = writeConnectionString;
                })
                .AddAccessor<IdentityDbContextAccessor>((options, optionsBuilder) =>
                {
                    var migrationsAssembly = typeof(IdentityDbContextAccessor).Assembly.GetName().Name;
                    optionsBuilder.UseSqlServer(options.DefaultTenant.DefaultConnectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddIdentity<IdentityDbContextAccessor>(options =>
                {
                    options.ConfigureCoreIdentity = core =>
                    {
                        core.Stores.MaxLengthForKeys = 128;
                    };
                })
                .AddIdentityApi();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseLibrameCore()
                .UseApi();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}